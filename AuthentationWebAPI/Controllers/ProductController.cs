using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthentationWebAPI.Controllers
{
    [Authorize] 
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var allProducts = _dbContext.Products.Select(p => new
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description
            }).ToList();

            if (allProducts.Count == 0)
            {
                return NotFound("No products found");
            }

            return Ok(allProducts);
        }


        [HttpGet("bycategory/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var productsInCategory = _dbContext.Products
                .Where(p => p.ProductCategory.Id == categoryId)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description
                })
                .ToList();


            if (productsInCategory.Count == 0)
            {
                return NotFound("No products found in the specified category");
            }

            return Ok(productsInCategory);
        }

            /* 
             * The code below is for the Question : 
            Add a Post endpoint that takes a single product and adds it to the database.
            It takes details for a single product and adds it to the database.
            */


            [HttpPost("add")]
        public ActionResult AddProduct([FromBody] Product productDto)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Description == productDto.ProductCategory.Description);

            if (category == null)
            {
                category = new Category { Description = productDto.ProductCategory.Description };
                _dbContext.Categories.Add(category);
            }

            var productToAdd = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                ProductCategory = category,
            };

            _dbContext.Products.Add(productToAdd);
            _dbContext.SaveChanges();

            return Ok(productToAdd.Name + " added successfully");
        }


    }
}
