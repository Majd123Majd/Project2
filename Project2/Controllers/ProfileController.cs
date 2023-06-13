using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Entities;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly AppDbContext _dbContext;

        public ProfileController(ILogger<ProfileController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // عرض بروفايل التاجر من وجهة نظر التاجر
        [HttpGet("/marketerprofile")]
        public IActionResult MarketerProfile()
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             int.TryParse(userId, out int marketeid);
           
            var marketer = _dbContext.Marketers
                    .Include(m => m.Posts) // تحميل المنشورات
                    .Include(m => m.Orders) // تحميل طلبات الشراء
                    .Include(m => m.Auctions) //تحميل 
                    .FirstOrDefault(m => m.Id == marketeid);

            var followedPages = _dbContext.FollowingPages
                .Where(fp => fp.userId == marketeid)//تحميل الصفحات المتابعة 
                .Select(fp => fp.Marketer);  
       

            if (marketer == null)
            {
                return NotFound();
            }


            return Ok(marketer);
        }
         // عرض بروفايل التاجر من وجهة نظر الزبون
        [HttpGet("/marketerprofile/{id}")]
        public IActionResult MarketerProfile(int id)
        {
            var marketer = _dbContext.Marketers
                    .Include(m => m.Posts) // تحميل المنشورات
                    .FirstOrDefault(m => m.Id == id);

            if (marketer == null)
            {
                return NotFound();
            }
            return View(marketer);
        }

        // تعديل معلومات الزبون/التاجر
        [HttpPost("/customerprofile/editprofile/{id}")]
        public IActionResult EditCustomerProfile(int id ,Customer Updatecustomer)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            // تحديث المعلومات بناءً على البيانات المحدثة
            existingCustomer.Name = Updatecustomer.Name;
            existingCustomer.phoneNumber = Updatecustomer.phoneNumber;
            existingCustomer.AddressId = Updatecustomer.AddressId;
            existingCustomer.photo = Updatecustomer.photo;
            existingCustomer.AccountCach = Updatecustomer.AccountCach;
            existingCustomer.Point = Updatecustomer.Point;

            _dbContext.SaveChanges();

            return Ok(Updatecustomer);
        }
        [HttpPost("/marketerprofile/editprofile/{id}")]
        public IActionResult EditMarketerProfile(int id, Marketer updatedMarketer)
        {
            var existingMarketer = _dbContext.Marketers.FirstOrDefault(m => m.Id == id);

            if (existingMarketer == null)
            {
                return NotFound();
            }

            // تحديث المعلومات بناءً على البيانات المحدثة
            existingMarketer.Name = updatedMarketer.Name;
            existingMarketer.phoneNumber = updatedMarketer.phoneNumber;
            existingMarketer.AddressId = updatedMarketer.AddressId;
            existingMarketer.photo = updatedMarketer.photo;
            existingMarketer.AccountCach = updatedMarketer.AccountCach;
            existingMarketer.Point = updatedMarketer.Point;

            // حفظ التغييرات في قاعدة البيانات
            _dbContext.SaveChanges();

            return Ok(updatedMarketer);
        }

      // متابعة التاجر
        [HttpPost("/marketerprofile/follow/{id}")]
        public IActionResult FollowMarketer(int id)
        {
            // الحصول على معرّف المستخدم الحالي
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // قم بتحويل معرّف المستخدم إلى int إذا كان من النوع int
            int.TryParse(userId, out int parsedUserId);

            // إنشاء كائن FollowingPage وتعيين القيم
            var followingPage = new FollowingPage
            {
                pageId = id,
                userId = parsedUserId,
                CreatedDate = DateTime.Now
            };

            // قم بإضافة التاجر إلى قاعدة البيانات (أو قم بتنفيذ الإجراء المناسب)
            _dbContext.FollowingPages.Add(followingPage);
            _dbContext.SaveChanges();

            // العودة برمز استجابة HTTP 200 للدلالة على نجاح العملية
             return Ok();

            //return RedirectToAction("MarketerProfile", new { id = marketerId });
        }

        // إلغاء متابعة التاجر
        [HttpPost("/marketerprofile/unfollow/{id}")]
        public IActionResult UnfollowMarketer(int id)
         {
             // الحصول على معرّف المستخدم الحالي
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // قم بتحويل معرّف المستخدم إلى int إذا كان من النوع int
            int.TryParse(userId, out int parsedUserId);

            // البحث عن كائن FollowingPage المرتبط بالتاجر والمستخدم
            var followingPage = _dbContext.FollowingPages.FirstOrDefault(fp => fp.pageId == id && fp.userId == parsedUserId);

            if (followingPage != null)
            {
                // قم بإزالة التاجر من قاعدة البيانات (أو قم بتنفيذ الإجراء المناسب)
                _dbContext.FollowingPages.Remove(followingPage);
                _dbContext.SaveChanges();
            }

            // العودة برمز استجابة HTTP 200 للدلالة على نجاح العملية
            return Ok();
           

            //return RedirectToAction("MarketerProfile", new { id = marketerId });
        }

        // عرض الصفحات المتابعة للمستخدم
        [HttpGet("/followingpages")]
        public IActionResult FollowingPages()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var followingPages = _dbContext.FollowingPages.Include(fp => fp.Marketer).Where(fp => fp.userId == int.Parse(userId)).ToList();
            return View(followingPages);
        }

        // عرض صفحة الزبون
        [HttpGet("/customerprofile/{id}")]
        public IActionResult CustomerProfile(int id)
        {
            var custfri =  _dbContext.Customers
                .Include(c => c.Friends) // تحميل قائمة الأصدقاء
                .FirstOrDefault(c => c.Id == id);

            var followedPages = _dbContext.FollowingPages
                .Where(fp => fp.userId == id)//تحميل الصفحات المتابعة 
                .Select(fp => fp.Marketer);  

            if (custfri == null)
            {
                return NotFound();
            }

            return View(custfri,followedPages);
        }

        private IActionResult View(Customer custfri, IQueryable<Marketer> followedPages)
        {
            throw new NotImplementedException();
        }

        // متابعة زبون آخر
        [HttpPost("/followuser/{friendId}")]
        public IActionResult FollowCustomer(int friendId)
        {
            var customerId = Convert.ToInt32(HttpContext.GetRouteValue("friendId"));
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var friend = _dbContext.Friends.FirstOrDefault(f => f.friendId == friendId && f.userId == int.Parse(userId));

            if (friend != null)
            {
                // الزبون متابع بالفعل
                return RedirectToAction("UserProfile", new { id = friendId });
            }

            var newFriend = new Friend
            {
                friendId = friendId,
                userId = int.Parse(userId),
                CreatedDate = DateTime.Now
            };

            _dbContext.Friends.Add(newFriend);
            _dbContext.SaveChanges();

            return RedirectToAction("UserProfile", new { id = friendId });
        }

    }
}
