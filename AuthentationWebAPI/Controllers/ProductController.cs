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
        private readonly AppDbContext appDbContext;

        public ProductController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        /* 
         *The following code is for the Question
         *Add a Get endpoint that returns all products.
         *It returns all products saved in the database
         */
        [HttpGet("/")]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var allProducts = appDbContext.Products.Select(p => new
            {
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.ProductCategory
            }).ToList();

            if (allProducts.Count == 0)
            {
                return NotFound("No products found in the database");
            }

            return Ok(allProducts);
        }

        /* 
         *The following code is for the Question
         *Add a Get endpoint that takes a category Id and returns all products in that category. 
         *It returns all products saved in the database
         */
        [HttpGet("bycategory/{categoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategoryId(int categoryId)
        {
            var productsInCategory = appDbContext.Products
                .Where(p => p.ProductCategory.Id == categoryId)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    Category = p.ProductCategory
                })
                .ToList();


            if (productsInCategory.Count == 0)
            {
                return NotFound("No products found in the category with id : "+categoryId);
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
            var category = appDbContext.Categories.FirstOrDefault(c => c.Description == productDto.ProductCategory.Description);

            if (category == null)
            {
                category = new Category { Description = productDto.ProductCategory.Description };
                appDbContext.Categories.Add(category);
            }

            var productToAdd = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Description = productDto.Description,
                ProductCategory = category,
            };
            Console.WriteLine(productToAdd);
            appDbContext.Products.Add(productToAdd);
            appDbContext.SaveChanges();

            return Ok(productToAdd.Name + " added successfully");
        }


    }
}
