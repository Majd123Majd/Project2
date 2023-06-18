using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Project2.Model;
using Project2.Model.Entities;
using System.IdentityModel.Tokens.Jwt;
using Project2.Repository.Interfaces;
using Project2.Model.Helpers;
using Project2.DTOs.NotifyDTOs;
using NuGet.Protocol.Plugins;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class NotifyController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotifyServices _notifyServices;

        public NotifyController(AppDbContext dbContext , IHttpContextAccessor httpContextAccessor = null, INotifyServices notifyServices = null)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _notifyServices = notifyServices;
        }
        [HttpGet("notifications")]
        public ActionResult GetNotifications()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _notifyServices.ViewNotificationsList(UserId);

            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("notifications/{id}")]
        public async Task<ActionResult> PostNotification(int id,AddNotificationViewModel notification)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response =  _notifyServices.AddNotification(UserId,id, notification);

            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("Searchlist")]
        public IActionResult GetSearch()
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _notifyServices.ViewSearchList(UserId);

            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("Searchlist")]
        public async Task<ActionResult> PostSearch(AddSearchViewModel search)
        {
            JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

            int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

            ApiResponse response = _notifyServices.AddSearch(UserId, search);

            if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("notification/{id}")]
        public IActionResult DeleteNotify(int id)
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

                int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                    .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

                ApiResponse response = _notifyServices.DeleteNotification(UserId, id);

                if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex +" ");
            }
        }

        [HttpDelete("search/{id}")]
        public IActionResult DeleteSearch(int id)
        {
            try
            {
                JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();

                int UserId = Convert.ToInt32(jwtHandler.ReadJwtToken(_httpContextAccessor.HttpContext
                    .Request.Headers["Authorization"].ToString().Split(" ")[1]).Claims.ToList()[0].Value);

                ApiResponse response = _notifyServices.DeleteSearch(UserId, id);

                if (!string.IsNullOrEmpty(response.ErrorMessage) ? response.ErrorMessage != "Seccess" : false)
                    return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex+" ");
            }
        }


    }
  
}