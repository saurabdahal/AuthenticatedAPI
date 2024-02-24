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

        /* 
         *The following code is for the Question
         *Add a Get endpoint that returns all products in the user's shopping cart. 
         *It returns all products currently in the user's shopping cart
         */

        [HttpGet("shoppingcart")]
        public ActionResult<IEnumerable<Product>> GetProductsInShoppingCart()
        {
            var userId = User.FindFirst(ClaimTypes.Email)?.Value;   // returns currently autheticated user
            var shoppingCart = appDbContext.ShoppingCarts
                    .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == userId);   // here we filter the shopping cart by user email; 

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found");
            }

            return Ok(shoppingCart.Products);
        }


        /* 
         *The following code is for the Question
         *Add a Post endpoint that takes a single ID and removes the item from the shopping cart.
         *It takes product id and removes the item from the shopping cart
        */

        [HttpPost("shoppingcart/remove-product/{productId}")]
        public ActionResult RemoveProductFromShoppingCart(int productId)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;  // currently authenticated user
            var shoppingCart = appDbContext.ShoppingCarts
                .Include(sc => sc.Products)
                .FirstOrDefault(sc => sc.User == email);

            var initialCount = shoppingCart?.Products?.Count ?? 0;

            if (shoppingCart == null)
            {
                return NotFound("Shopping cart not found");
            }

            var productToRemove = shoppingCart.Products.FirstOrDefault(p => p.Id == productId); // fetches the product record by ID

            if (productToRemove == null)
            {
                return NotFound("Product not found in the shopping cart");
            }
            shoppingCart.Products.Remove(productToRemove);
            appDbContext.SaveChanges();

            return Ok($"Item removed. There were {initialCount} items in your shopping cart, now there are {GetProductsInShoppingCart(shoppingCart.Id)} items left");
        }

        /* 
         *The following code is for the Question
         *Add a Post endpoint that takes a single ID and adds the item to the shopping cart. Make sure to create the Shopping Cart if needed and Assign the Current Users Email to the User property.
         *It takes product id and attempts to add the item to the shopping cart, if the shopping is not created yet, it creates the shopping cart and adds the product
        */

        [HttpPost("shoppingcart/add-product/{productId}")]
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


            return Ok("Product " + productToAdd.Name + " added successfully to your shopping cart.\n Your shopping cart now has " + GetProductsInShoppingCart(shoppingCart.Id) + " items.");
        }

        /*
         * The purpose of this method is to get the number of products in your shopping cart
         */
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
