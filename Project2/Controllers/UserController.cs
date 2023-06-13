using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Project2.Model;
using Project2.Model.DTOs;
using Project2.Model.DTOs.CustomerDTOs;
using Project2.Model.Entities;
using Project2.Repository.Interfaces;
using Project2.Repository.Services;
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
        private IUserService _UserService;
        public UserController(AppDbContext DbContext, IUserService UserService)
        {
            _DbContext = DbContext;
            _UserService = UserService;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel Login)
        {
            var Response = _UserService.Login(Login);
            return Ok(Response);
        }
        [HttpPost("RegisterForCustomer")]
        public async Task<IActionResult> RegisterForCustomer(AddCustomerViewModel NewCustomer)
        {
            var Response = await _UserService.RegisterForCustomer(NewCustomer);
            return Ok(Response);
        }

        //[HttpPost("Register")]
        //public string Register(RegisterViewModel NewUser)
        //{
        //    bool CheckIfNameIsAlreadyExist = _DbContext.Users
        //        .Any(x => x.Name.ToLower() == NewUser.Name.ToLower() ||
        //            x.Email.ToLower() == NewUser.Email.ToLower());

        //    if (CheckIfNameIsAlreadyExist)
        //        return "Invalid Name or Email";

        //    User NewUserEntity = new User()
        //    {
        //        Name = NewUser.Name,
        //        Email = NewUser.Email,
        //        userType = NewUser.userType
        //    };
        //    //save as custmer or seller
        //    //if (NewUser.userType.Equals())
        //    //{
        //    //    Address address = new Address()
        //    //    {
        //    //        city = NewUser.city,
        //    //        state = NewUser.state,
        //    //    };
        //    //    Customer customer = new Customer()
        //    //    {
        //    //        Name = NewUser.Name,
        //    //        phoneNumber = NewUser.phonNumber,
        //    //        age = NewUser.age,
        //    //        AddressId = address.Id,
        //    //        //userId = NewUser.UID

        //    //    };
        //    //    _DbContext.Addresses.Add(address);
        //    //    _DbContext.Customers.Add(customer);
        //    //    _DbContext.SaveChanges();
        //    //}

        //    byte[] Salt = new byte[16] { 41, 214, 78, 222, 28, 87, 170, 211, 217, 125, 200, 214, 185, 144, 44, 34 };

        //    // Derive a 256-bit Subkey (Use HMACSHA256 With 100,000 Iterations)
        //    NewUserEntity.password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: NewUser.password,
        //        salt: Salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 100000,
        //        numBytesRequested: 256 / 8));

        //    _DbContext.Users.Add(NewUserEntity);
        //   // _DbContext.Customers.Add(customer);
        //    _DbContext.SaveChanges();

        //    List<Claim> Claims = new List<Claim> {
        //        new Claim("UserId", NewUserEntity.UID.ToString()),
        //        new Claim("RandomGuid", Guid.NewGuid().ToString()),
        //        // new Claim("Role", Role)
        //    };

        //    SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["JWT:Key"]));
        //    SigningCredentials Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

        //    JwtSecurityToken TokenDetails = new JwtSecurityToken("http://localhost:53174/",
        //      "http://localhost:53174/",
        //      Claims,
        //      expires: DateTime.Now.AddMinutes(30),
        //      signingCredentials: Credentials);

        //    string TokenString = new JwtSecurityTokenHandler().WriteToken(TokenDetails);

        //    return TokenString;
        //}
    }
    
}
