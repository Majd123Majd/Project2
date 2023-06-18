using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Project2.Data.Enum;
using Newtonsoft.Json;
using Project2.Model;
using Project2.Model.Entities;
using Project2.DTOs;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly AppDbContext _dbContext;
        private RegistrationStep currentStep;
        private UserType selectedUserType;


        public AccountController(UserManager<User> userManager, IAuthenticationService authenticationService,AppDbContext dbContext)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
            _dbContext = dbContext;
        }
        //[HttpPost("registertype")]
        //public IActionResult SelectUserType([FromBody] UserTypeSelectionModel model)
        //{
        //    selectedUserType = model.AccountType;
        //    currentStep = RegistrationStep.UserInformationInput;
        //    return Ok();
        //}
        [HttpPost("register")]
        public IActionResult EnterUserInformation([FromBody] UserInformationModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                password = model.password,
                userType = selectedUserType
            };
            // قم بمعالجة بيانات المستخدم حسب النوع المحدد
            if (selectedUserType == UserType.Customer)
            {
                var customer = new Customer
                {
                    Name = model.Name,
                    //age = model.age,
                    userId = user.UID,
                    phoneNumber = model.phoneNumber,
                   // Address = new Address
                    
                    city = model.city,
                    zone = model.state
                    
                };
                _dbContext.Users.Add(user);
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Home", new { userType = selectedUserType });

            }
            else if (selectedUserType == UserType.Marketer)
            {
                var marketer = new Marketer
                {
                    Name = model.Name,
                    phoneNumber = model.phoneNumber,
                    userId = user.UID,
                    
                    city = model.city,
                    zone = model.state
                    
                };
                // قم بحفظ بيانات البائع في قاعدة البيانات
                _dbContext.Users.Add(user);
                _dbContext.Marketers.Add(marketer);
                _dbContext.SaveChanges();
                // وارجاع الاستجابة المناسبة
                return RedirectToAction("Index", "Home", new { userType = selectedUserType });

            }

            // إرجاع استجابة افتراضية في حالة عدم تحديد النوع المحدد
            return BadRequest("Invalid user type");
        }

   
        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var result = await _authenticationService.SignInAsync(model.Email, model.Password);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }

        //    return Unauthorized();
        //}


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user_type = IsValidUser(model.Email, model.Password);
            // التحقق من صحة البريد الإلكتروني وكلمة المرور
            if (user_type == null)
            {
                return BadRequest("Invalid email or password");
            }
            else
            {
                // حفظ بيانات تسجيل الدخول الحالية في الجلسة
                HttpContext.Session.SetString("CurrentLoginModel", JsonConvert.SerializeObject(model));

                // التحقق من نوع المستخدم وإعادة الواجهة المناسبة
                if (user_type == UserType.Customer)
                {
                    // إعادة الواجهة للزبون
                    return RedirectToAction("Index", "Home", new { userType = UserType.Customer });
                }
                else if (user_type == UserType.Marketer)
                {
                    // إعادة الواجهة للبائع
                    return RedirectToAction("Index", "Home", new { userType = UserType.Marketer });
                }
            }
            // في حالة عدم صحة بيانات الدخول
            return BadRequest("Invalid email or password");
        }
        private UserType? IsValidUser(string email, string password)
        {
            var user = _dbContext.Users.Where(u => u.Email == email
            && u.password == password ).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            var usertype = user.userType;
            return usertype;
        }

        HttpContext context;
        AuthenticationProperties p;
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOutAsync(context,"", p);

            return Ok();
        }
    }
}
