using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AuthentationWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDbContext appDbContext;

        public ShoppingCartController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        [HttpGet("shopping-cart")]
        public ActionResult<IEnumerable<Product>> GetProductsInShoppingCart()
        {
            var userId = User.FindFirst(ClaimTypes.Email)?.Value;
            var shoppingCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userId);

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found");
            }

            return Ok(shoppingCart.Products);
        }

        [HttpPost("shopping-cart/add/{productId}")]
        public ActionResult AddProductToShoppingCart(int productId)
        {

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var shoppingCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userEmail);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    User = userEmail ?? "",
                    Products = new List<Product>()
                };
                appDbContext.ShoppingCarts.Add(shoppingCart);
            }

            var productToAdd = appDbContext.Products.Find(productId);

            if (productToAdd == null)
            {
                return NotFound("Product not found");
            }

            shoppingCart.Products.Add(productToAdd);
            appDbContext.SaveChanges();


            return Ok("Product " + productToAdd.Name + " added successfully to your shopping cart.\n Your shopping cart now has "+GetProductsInShoppingCart(shoppingCart.Id)+" items.");
        }

        private int GetProductsInShoppingCart(int shoppingCartId)
        {
            var count = appDbContext.ShoppingCarts
       .Where(cart => cart.Id == shoppingCartId)
       .SelectMany(cart => cart.Products) 
       .Count();

            return count;
        }
    }
}
