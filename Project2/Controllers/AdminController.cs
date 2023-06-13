using Microsoft.AspNetCore.Mvc;
using Project2.Data;
using Project2.Models;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AdminController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("allUser")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _dbContext.Users.ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }

        [HttpGet("allMarkter")]
        public IActionResult GetMarkter()
        {
            try
            {
                var markter = _dbContext.Marketers.ToList();
                return Ok(markter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }

        [HttpGet("allComplaint")]
        public IActionResult GetComplaint()
        {
            try
            {
                var complaints = _dbContext.Complaints.ToList();
                return Ok(complaints);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpGet("allDeliver")]
        public IActionResult GetDeliver()
        {
            try
            {
                var delivers = _dbContext.Delivers.ToList();
                return Ok(delivers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpGet("allRole")]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _dbContext.Roles.ToList();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpGet("allMaintainer")]
        public IActionResult GetMaintainer()
        {
            try
            {
                var maintainers= _dbContext.Maintainers.ToList();
                return Ok(maintainers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpPost("addrole")]
        public IActionResult PostRole(String rolename)
        {
            try 
            {
              var role = new Role
                {
                    Name = rolename,
                };

            // إضافة المنتج إلى قاعدة البيانات
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();

            return Ok(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpPost("addmaintaner")]
        public IActionResult PostMaintaner(String name,String email,int roleid)
        {
            try
            {
                var maintainer = new Maintainer
                {
                    Name = name,
                    Email = email,
                    RoleId = roleid
                };

                // إضافة المنتج إلى قاعدة البيانات
                _dbContext.Maintainers.Add(maintainer);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
        [HttpPost("addDeliver")]
        public IActionResult PostDeliver(String name, String email, int phonNum , int addid)
        {
            try
            {
                var user = new User
                {
                    Name = name,
                    Email = email,

                };
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                var deliver = new Deliver
                {
                    Name = name,
                    phoneNumber = phonNum,
                    AddressId = addid,
                    userId = user.UID
                };

                // إضافة المنتج إلى قاعدة البيانات
                _dbContext.Delivers.Add(deliver);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString);
            }

        }
    }
}
