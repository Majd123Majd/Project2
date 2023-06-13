using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using Project2.Model;
using Project2.Model.DTOs;
using Project2.Model.DTOs.CustomerDTOs;
using Project2.Model.DTOs.DeliverDTOs;
using Project2.Model.Entities;
using Project2.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project2.Repository.Services
{
    public class UserService: IUserService
    {
        public AppDbContext _DbContext { get; set; }
        private IConfiguration _Config;
        private IMapper _Mapper;

        public UserService(AppDbContext DbContext, IConfiguration Config, IMapper Mapper)
        {
            _DbContext = DbContext;
            _Config = Config;
            _Mapper = Mapper;
        }
        public string Login(LoginViewModel Login)
        {
            User? User = _DbContext.Users
                .FirstOrDefault(x => x.Name.ToLower() == Login.UserName.ToLower());

            if (User == null)
                return "Invalid User Name";

            //string Role = string.Empty;

            //bool CheckIfItsCustomer = _DbContext.Customers
            //    .Any(x => x.userId == User.UID);

            //if (CheckIfItsCustomer)
            //    Role = "Customer";

            //else
            //{
            //    bool CheckIfItsMarketer = _DbContext.Marketers
            //        .Any(x => x.userId == User.UID);

            //    if (CheckIfItsMarketer)
            //        Role = "Marketer";

            //    else
            //        Role = "Deliver";
            //}

            byte[] Salt = new byte[16] { 41, 214, 78, 222, 28, 87, 170, 211, 217, 125, 200, 214, 185, 144, 44, 34 };

            // Derive a 256-bit Subkey (Use HMACSHA256 With 100,000 Iterations)
            string Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: Login.Password,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            bool Verified = (User.password == Password);

            if (!Verified)
                return "Invalid Password";

            List<Claim> Claims = new List<Claim> {
                new Claim("UserId", User.UID.ToString()),
                new Claim("RandomGuid", Guid.NewGuid().ToString()),
                new Claim("UserName", User.Name),
                // new Claim("Role", Role)
            };

            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:Key"]));
            SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken TokenDetails = new JwtSecurityToken("http://localhost:53174/",
              "http://localhost:53174/",
              Claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: Credentials);

            string TokenString = new JwtSecurityTokenHandler().WriteToken(TokenDetails);

            return TokenString;
        }
        public async Task<string> RegisterForCustomer(AddCustomerViewModel NewCustomer)
        {
            User NewUserEntity = _Mapper.Map<User>(NewCustomer.UserInformation);
            NewUserEntity.userType = Data.Enum.UserType.Customer;

            byte[] Salt = new byte[16] { 41, 214, 78, 222, 28, 87, 170, 211, 217, 125, 200, 214, 185, 144, 44, 34 };

            // Derive a 256-bit Subkey (Use HMACSHA256 With 100,000 Iterations)
            NewUserEntity.password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: NewCustomer.UserInformation.password,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            await _DbContext.Users.AddAsync(NewUserEntity);
            await _DbContext.SaveChangesAsync();

            Customer NewCustomerEntity = _Mapper.Map<Customer>(NewCustomer);

            NewCustomerEntity.userId = NewUserEntity.UID;

            _DbContext.Customers.Add(NewCustomerEntity);
            _DbContext.SaveChanges();

            return Login(new LoginViewModel()
            {
                UserName = NewCustomer.Name,
                Password = NewCustomer.UserInformation.password
            });
        }
        public async Task<string> RegisterForDeliver(AddDeliverViewModel NewDeliver)
        {

        }
    }
}
