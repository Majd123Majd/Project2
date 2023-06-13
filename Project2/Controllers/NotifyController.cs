using Microsoft.AspNetCore.Mvc;
using Project2.Data;
using Project2.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class NotifyController : Controller
    {
        private readonly AppDbContext _dbContext;

        public NotifyController(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        [HttpGet("GetNotifications")]
        //[Route("notification")]
        public ActionResult GetNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int usid);
            var notifications = _dbContext.Notifications
                .Where(s => s.User.UID == usid)
                .ToList();

            if (notifications == null)
            {
                return NotFound();
            }

            return View();
        }
        [HttpPost("{notification}")]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotifications), new { id = notification.Id }, notification);
        }

        [HttpGet("SearchList")]
        public IActionResult GetSearch()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userId, out int usid);
            var searchResults = _dbContext.Searches
                .Where(s => s.User.UID == usid)
                .ToList();

            return View(searchResults);
        }
        [HttpPost("{search}")]
        public async Task<ActionResult<Search>> PostSearch(Search search)
        {
            _dbContext.Searches.Add(search);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSearch), new { id = search.Id }, search);
        }
        [HttpDelete("notification/{id}")]
        public IActionResult DeleteNotify(int id)
        {
            try
            {
                var notification = _dbContext.Notifications.Find(id);

                if (notification == null)
                    return NotFound();

                _dbContext.Notifications.Remove(notification);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, " ");
            }
        }

        [HttpDelete("search/{id}")]
        public IActionResult DeleteSearch(int id)
        {
            try
            {
                var search = _dbContext.Searches.Find(id);

                if (search == null)
                    return NotFound();

                _dbContext.Searches.Remove(search);
                _dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, " ");
            }
        }


    }
  
}