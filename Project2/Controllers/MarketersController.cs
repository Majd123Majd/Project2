using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.DTOs.CustomerDTOs;
using Project2.Model;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;
using Project2.Repository.Services;

namespace Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketersController : ControllerBase
    {
        private IMarketerService _MarketerService;
        public MarketersController(IMarketerService MarketerService)
        {
            _MarketerService = MarketerService;
        }
        [HttpPost("GetAllMarketers")]
        public IActionResult GetAllMarketers(ComplexFilter Filter)
        {
            ApiResponse Response = _MarketerService.GetAllMarketers(Filter);
            return Ok(Response);
        }
    }
}
