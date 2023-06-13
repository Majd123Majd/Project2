using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Project2.Data;
using Project2.Models;
using Project2.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _DbContext;
        private IConfiguration _Config;

        public UserController(AppDbContext DbContext, IConfiguration Config)
        {
            _DbContext = DbContext;
            _Config = Config;
        }
        [HttpPost("Login")]
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
        [HttpPost("Register")]
        public string Register(RegisterViewModel NewUser)
        {
            bool CheckIfNameIsAlreadyExist = _DbContext.Users
                .Any(x => x.Name.ToLower() == NewUser.Name.ToLower() ||
                    x.Email.ToLower() == NewUser.Email.ToLower());

            if (CheckIfNameIsAlreadyExist)
                return "Invalid Name or Email";

            User NewUserEntity = new User()
            {
                Name = NewUser.Name,
                Email = NewUser.Email,
                userType = NewUser.userType
            };
            //save as custmer or seller
            //if (NewUser.userType.Equals())
            //{
            //    Address address = new Address()
            //    {
            //        city = NewUser.city,
            //        state = NewUser.state,
            //    };
            //    Customer customer = new Customer()
            //    {
            //        Name = NewUser.Name,
            //        phoneNumber = NewUser.phonNumber,
            //        age = NewUser.age,
            //        AddressId = address.Id,
            //        //userId = NewUser.UID

            //    };
            //    _DbContext.Addresses.Add(address);
            //    _DbContext.Customers.Add(customer);
            //    _DbContext.SaveChanges();
            //}

            byte[] Salt = new byte[16] { 41, 214, 78, 222, 28, 87, 170, 211, 217, 125, 200, 214, 185, 144, 44, 34 };

            // Derive a 256-bit Subkey (Use HMACSHA256 With 100,000 Iterations)
            NewUserEntity.password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: NewUser.password,
                salt: Salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            _DbContext.Users.Add(NewUserEntity);
           // _DbContext.Customers.Add(customer);
            _DbContext.SaveChanges();

            List<Claim> Claims = new List<Claim> {
                new Claim("UserId", NewUserEntity.UID.ToString()),
                new Claim("RandomGuid", Guid.NewGuid().ToString()),
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
    }
    
}
