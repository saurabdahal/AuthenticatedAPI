using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthentationWebAPI.Controllers
{
    [Authorize] // Apply authorization attribute to require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = "me";
            return Ok(products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

       
    }
}
