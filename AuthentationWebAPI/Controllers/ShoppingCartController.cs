using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthentationWebAPI.Controllers
{
    [Authorize] // Apply authorization attribute to require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDBContext _dbContext;

        public ShoppingCartController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetShoppingCart()
        {
            // Get the current user's shopping cart based on user identity
            var userId = User.FindFirst("sub")?.Value; // Assuming you're using JWT and 'sub' claim for user id
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found for the user.");
            }

            // Return the products in the shopping cart
            return Ok(shoppingCart.Products.Select(p => p.Product));
        }

        // POST: api/ShoppingCart/RemoveItem
        [HttpPost("RemoveItem")]
        public IActionResult RemoveItemFromCart(int productId)
        {
            // Get the current user's shopping cart based on user identity
            var userId = User.FindFirst("sub")?.Value; // Assuming you're using JWT and 'sub' claim for user id
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found for the user.");
            }

            // Find the product in the shopping cart and remove it
            var productInCart = shoppingCart.Products.FirstOrDefault(p => p.ProductId == productId);
            if (productInCart != null)
            {
                shoppingCart.Products.Remove(productInCart);
                _dbContext.SaveChanges();
                return Ok("Item removed from the shopping cart.");
            }

            return NotFound("Product not found in the shopping cart.");
        }

        [HttpPost("AddItem")]
        public IActionResult AddItemToCart(int productId)
        {
            // Get the current user's email
            var userEmail = User.Identity.Name;

            // Get or create the shopping cart for the user
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userEmail);

            if (shoppingCart == null)
            {
                // Create a new shopping cart for the user
                shoppingCart = new ShoppingCart
                {
                    User = userEmail,
                    Products = new List<ShoppingCartProduct>()
                };
                _dbContext.ShoppingCarts.Add(shoppingCart);
            }

            // Check if the product is already in the cart
            var productInCart = shoppingCart.Products.FirstOrDefault(p => p.ProductId == productId);

            if (productInCart == null)
            {
                // Product not in the cart, add it
                shoppingCart.Products.Add(new ShoppingCartProduct { ProductId = productId });
                _dbContext.SaveChanges();
                return Ok("Item added to the shopping cart.");
            }

            return Conflict("Product already exists in the shopping cart.");
        }
    }
}
