using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Project2.Model;
using Project2.DTOs;
using Project2.DTOs.CustomerDTOs;
using Project2.DTOs.DeliverDTOs;
using Project2.DTOs.MarketerDTOs;
using Project2.Model.Entities;
using Project2.Model.Helpers;
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
        private IUserService _UserService;
        public UserController(IUserService UserService)
        {
            _UserService = UserService;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginViewModel Login)
        {
            ApiResponse Response = _UserService.Login(Login);
            return Ok(Response);
        }
        [HttpPost("RegisterForCustomer")]
        public async Task<IActionResult> RegisterForCustomer(AddCustomerViewModel NewCustomer)
        {
            ApiResponse Response = await _UserService.RegisterForCustomer(NewCustomer);
            return Ok(Response);
        }
        [HttpPost("RegisterForDeliver")]
        public async Task<IActionResult> RegisterForDeliver(AddDeliverViewModel NewDeliver)
        {
            ApiResponse Response = await _UserService.RegisterForDeliver(NewDeliver);
            return Ok(Response);
        }
        [HttpPost("RegisterForMarketer")]
        public async Task<IActionResult> RegisterForMarketer(AddMarketerViewModel NewMarketer)
        {
            ApiResponse Response = await _UserService.RegisterForMarketer(NewMarketer);
            return Ok(Response);
        }
    }
}
