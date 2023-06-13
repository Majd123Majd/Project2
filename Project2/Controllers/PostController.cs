using Microsoft.AspNetCore.Mvc;
using Project2.Data;
using Project2.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PostController : Controller
    {
        private readonly AppDbContext _dbContext;

        public PostController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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

        [HttpGet("seller/{sellerId}")]
        public IActionResult GetSellerPosts(int sellerId)
        {
            try
            {
                var sellerPosts = _dbContext.Posts.Where(p => p.marketerId == sellerId).ToList();
                return Ok(sellerPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء جلب المنشورات للبائع.");
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
                    Quantity = productQuantity,
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
                return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء إضافة المنتج والمنشور.");
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
                return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء تحديث المنتج.");
            }
        }

        [HttpDelete("deletepost/{id}")]
        public IActionResult DeletePost(int postId)
        {
            try
            {
                // البحث عن المنشور المراد حذفه بواسطة الـ postId
                var post = _dbContext.Posts.Find(postId);

                if (post == null)
                    return NotFound("المنشور غير موجود.");

                // حذف المنشور من قاعدة البيانات
                _dbContext.Posts.Remove(post);
                _dbContext.SaveChanges();

                return Ok("تم حذف المنتج بنجاح.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء حذف المنتج.");
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
