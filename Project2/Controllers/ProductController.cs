using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project2.DTOs;
using Project2.Model.Helpers;
using Project2.Repository.Interfaces;

namespace Project2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }
        [HttpPost("GetAllProducts")]
        public IActionResult GetAllProducts(ComplexFilter Filter)
        {
            ApiResponse Response = _ProductService.GetAllProducts(Filter);
            return Ok(Response);
        }
    }
}
