using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Entities;
using Project2.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Project2.Model.Helpers;
using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.DTOs.DeliverDTOs;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly AppDbContext _dbContext;
        private readonly IProfileService _profileService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ProfileController(ILogger<ProfileController> logger, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IProfileService profileService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _profileService = profileService;
        }

        // عرض بروفايل التاجر 
        [HttpGet("marketerprofile")]
        public IActionResult MarketerProfile()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ViewMarketerProfile(UserId);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }
       
        // عرض صفحة الزبون
        [HttpGet("customerprofile")]
        public IActionResult CustomerProfile()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ViewCustomerProfile(UserId);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //صفحة شركة التوصيل
        [HttpGet("deliverprofile")]
        public IActionResult DeliverProfile()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ViewDeliverProfile(UserId);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }
       
        // تعديل معلومات الزبون
        [HttpPost("editcustomerprofile/{id}")]
        public IActionResult EditCustomerProfile(int id ,UpdateCustomerViewModel Updatecustomer)
        {
            ApiResponse response = _profileService.EditCustomerProfile(id, Updatecustomer);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
            // تحديث المعلومات بناءً على البيانات المحدثة
            // existingCustomer.Name = Updatecustomer.Name;
            // existingCustomer.phoneNumber = Updatecustomer.phoneNumber;
            // //existingCustomer.AddressId = Updatecustomer.AddressId;
            // existingCustomer.photo = Updatecustomer.photo;
            //// existingCustomer.AccountCach = Updatecustomer.AccountCach;
            // existingCustomer.Point = Updatecustomer.Point;
            //_dbContext.SaveChanges();
            //return Ok(Updatecustomer);
        }

        //تعديل معلومات التاجر
        [HttpPost("editmarketerprofile/{id}")]
        public IActionResult EditMarketerProfile(int id, UpdateMarketerViewModel updatedMarketer)
        {
            ApiResponse response = _profileService.EditMarketerProfile(id, updatedMarketer);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
            // var existingMarketer = _dbContext.Marketers.FirstOrDefault(m => m.Id == id);

            // if (existingMarketer == null)
            // {
            //     return NotFound();
            // }

            // // تحديث المعلومات بناءً على البيانات المحدثة
            // existingMarketer.Name = updatedMarketer.Name;
            // existingMarketer.phoneNumber = updatedMarketer.phoneNumber;
            // //existingMarketer.AddressId = updatedMarketer.AddressId;
            // existingMarketer.photo = updatedMarketer.photo;
            //// existingMarketer.AccountCach = updatedMarketer.AccountCach;
            // existingMarketer.Point = updatedMarketer.Point;

            // // حفظ التغييرات في قاعدة البيانات
            // _dbContext.SaveChanges();

            // return Ok(updatedMarketer);
        }

        //تعديل معلومات التوصيل
        [HttpPost("editdeliverprofile/{id}")]
        public IActionResult EditDriverProfile(int id, UpdateDeliverViewModel updatedDeliver)
        {
            ApiResponse response = _profileService.EditDeliverProfile(id, updatedDeliver);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
            
        }

        // متابعة التاجر
        [HttpPost("followmarketer/{id}")]
        public IActionResult FollowMarketer(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.FollowMarketer(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);

        }

        // إلغاء متابعة التاجر
        [HttpPost("unfollowmarketer/{id}")]
        public IActionResult UnfollowMarketer(int id)
         {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.UnfollowMarketer(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        // متابعة زبون آخر
        [HttpPost("followcustomer/{id}")]
        public IActionResult FollowCustomer(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.FollowCustomer(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //إلغاء متابعة زبون
        [HttpPost("unfollowcustomer/{id}")]
        public IActionResult UnfollowCustomer(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.UnfollowCustomer(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        // عرض الصفحات المتابعة للمستخدم
        [HttpGet("followingpages")]
        public IActionResult FollowingPages()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ViewFollowingPagesList(UserId);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }
        
        //عرص الاصدقاء
        [HttpGet("friendslist")]
        public IActionResult FriendsList()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ViewFriendsList(UserId);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        // عرض صفحة الزبون
        [HttpGet("customerprofile/{id}")]
        public IActionResult CustomerProfile(int id)
        {
            ApiResponse response = _profileService.ViewCustomerProfile(id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        // عرض بروفايل التاجر من وجهة نظر الزبون
        [HttpGet("marketerprofile/{id}")]
        public IActionResult MarketerProfile(int id)
        {
            ApiResponse response = _profileService.ViewMarketerProfile(id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //صفحة شركة التوصيل
        [HttpGet("deliverprofile/{id}")]
        public IActionResult DeliverProfile(int id)
        {
            ApiResponse response = _profileService.ViewDeliverProfile(id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //التعاقد مع شركة توصيل
        [HttpPost("contactdeliver/{id}")]
        public IActionResult ContractWithDeliver(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.ContractWithDeliver(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }
        // الغاء التعاقد مع شركة توصيل
        [HttpPost("cancelcontactdeliver/{id}")]
        public IActionResult CancelContractWithDeliver(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.CancelContractWithDeliver(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //
        [HttpPost("addcustomeragent/{id}")]
        public IActionResult AddAgent(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.AddAgent(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        //
        [HttpPost("removecustomeragent/{id}")]
        public IActionResult RemoveAgent(int id)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _profileService.RemoveAgent(UserId, id);
            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
