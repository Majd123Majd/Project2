using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;
using Project2.Repository.Services;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostServices _postServices;
        public PostController(AppDbContext dbContext , IHttpContextAccessor httpContextAccessor , IPostServices postServices)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _postServices = postServices;
        }
        [HttpGet("post/{id}")]
        public IActionResult PostDetails(int id)
        {
            var post = _dbContext.Posts
                .Include(p => p.Marketer) // تحميل معلومات التاجر
                .Include(p => p.Product) // تحميل معلومات المنتج
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpGet("marketerpost")]
        public IActionResult GetSellerPosts()
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

                int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                    .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

                ApiResponse response = _postServices.ViewPostForMarketer(UserId);

                if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex +"حدث خطأ أثناء جلب المنشورات للبائع.");
            }
        }

        [HttpGet("customerpost")]
        public IActionResult GetcustomerPosts()
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

                int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                    .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

                ApiResponse response = _postServices.ViewPostForCustomer(UserId);

                if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "حدث خطأ أثناء جلب المنشورات للبائع.");
            }
        }

        [HttpPost("addpost")]
        public IActionResult AddPost(int marketerId, string productName, int productPrice, int productQuantity, string description)
        {
            try
            {
                // إنشاء منتج جديد
                var product = new Product
                {
                    Name = productName,
                    Price = productPrice,
                    //Quantity = productQuantity,
                    CreatedDate = DateTime.Now
                };

                // إضافة المنتج إلى قاعدة البيانات
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();

                // إنشاء منشور جديد
                var post = new Post
                {
                    marketerId = marketerId,
                    productId = product.id, // استخدام معرّف المنتج الجديد
                    Description = description,
                    CreatedDate = DateTime.Now
                };

                // إضافة المنشور إلى قاعدة البيانات
                _dbContext.Posts.Add(post);
                _dbContext.SaveChanges();

                return Ok("تمت إضافة المنتج والمنشور بنجاح.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex+ "حدث خطأ أثناء إضافة المنتج والمنشور.");
            }
        }

        [HttpPut("updatepost/{id}")]
        public IActionResult UpdatePost(int postId, int marketerId, int productId, string description)
        {
            try
            {
                // البحث عن المنشور المراد تحديثه بواسطة الـ postId
                var post = _dbContext.Posts.Find(postId);

                if (post == null)
                    return NotFound("المنشور غير موجود.");

                // تحديث بيانات المنشور
                post.marketerId = marketerId;
                post.productId = productId;
                post.Description = description;

                // حفظ التغييرات في قاعدة البيانات
                _dbContext.SaveChanges();

                return Ok("تم تحديث المنتج بنجاح.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex+"حدث خطأ أثناء تحديث المنتج.");
            }
        }

        [HttpDelete("deletepost/{id}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                ApiResponse response = _postServices.DeletePost(id);

                if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "حدث خطأ أثناء حذف المنتج.");
            }
        }

        [HttpPost("interaction")]
        public IActionResult AddInteraction(int postId, string reactionType)
        {
            // الحصول على معرّف المستخدم الحالي
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // قم بتحويل معرّف المستخدم إلى int إذا كان من النوع int
            int.TryParse(userId, out int parsedUserId);

            // إنشاء كائن Interaction وتعيين القيم
            var interaction = new Interaction
            {
                PostId = postId,
                UserId = parsedUserId,
                ReactionType = reactionType,
                CreatedDate = DateTime.Now
            };

            // قم بإضافة التفاعل إلى قاعدة البيانات (أو قم بتنفيذ الإجراء المناسب)
            _dbContext.Interactions.Add(interaction);
            _dbContext.SaveChanges();

            // العودة برمز استجابة HTTP 200 للدلالة على نجاح العملية
            return Ok();
        }



    }
}
