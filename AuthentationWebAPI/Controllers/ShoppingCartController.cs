using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthentationWebAPI.Controllers
{
    [Authorize] // Apply authorization attribute to require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ShoppingCartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("shopping-cart")]
        [Authorize] // Requires authentication
        public ActionResult<IEnumerable<Product>> GetProductsInShoppingCart()
        {
            // Retrieve the current user's shopping cart based on user identity
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products) // Include products in the response
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found");
            }

            return Ok(shoppingCart.Products);
        }

        [HttpPut("shopping-cart/remove/{productId}")]
        [Authorize] // Requires authentication
        public ActionResult RemoveProductFromShoppingCart(int productId)
        {
            // Retrieve the current user's shopping cart based on user identity
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found");
            }

            // Find the product in the shopping cart
            var productToRemove = shoppingCart.Products.FirstOrDefault(p => p.Id == productId);

            if (productToRemove == null)
            {
                return NotFound("Product not found in the shopping cart");
            }

            // Remove the product from the shopping cart
            shoppingCart.Products.Remove(productToRemove);
            _dbContext.SaveChanges();

            return NoContent(); // 204 No Content
        }

        [HttpPost("shopping-cart/add/{productId}")]
        [Authorize] // Requires authentication
        public ActionResult AddProductToShoppingCart(int productId)
        {

            // Retrieve the current user's information based on user identity
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            // Find or create the user's shopping cart
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                // Create a new shopping cart if not found
                shoppingCart = new ShoppingCart
                {
                    User = userEmail ?? "",
                    Products = new List<Product>()
                };
                _dbContext.ShoppingCarts.Add(shoppingCart);
            }

            // Find the product by ID
            var productToAdd = _dbContext.Products.Find(productId);

            if (productToAdd == null)
            {
                return NotFound("Product not found");
            }

            // Add the product to the shopping cart
            shoppingCart.Products.Add(productToAdd);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProductsInShoppingCart), new { }, shoppingCart.Products);
        }
    }
}
