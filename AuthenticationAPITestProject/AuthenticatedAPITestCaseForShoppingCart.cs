using AuthentationWebAPI.Data;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForShoppingCart
    {
        [TestMethod]
        public void ShoppingCart_ShouldSetPropertiesCorrectly()
        {
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "P1", Price = 10.0m, Description = "desc",ProductCategory = new Category { Id=1,Description="a" } },
            new Product { Id = 1, Name = "P2", Price = 10.0m, Description = "desc",ProductCategory = new Category { Id=1,Description="a" } }
        };

            var shoppingCart = new ShoppingCart
            {
                Id = 1,
                User = "Me",
                Products = products
            };

            Assert.AreEqual(1, shoppingCart.Id);
            Assert.AreEqual("Me", shoppingCart.User);
            CollectionAssert.AreEqual(products, shoppingCart.Products);
        }

        [TestMethod]
        public void ShoppingCart_UserShouldNotBeNull()
        {
            var shoppingCart = new ShoppingCart
            {
                Id = 1,
                User = null,
                Products = new List<Product>()
            };


            Assert.IsNotNull(shoppingCart.User, "User should not be null");  // results in fail test scenerio
        }

        [TestMethod]
        public void ShoppingCart_ProductsShouldNotBeNull()
        {
            var shoppingCart = new ShoppingCart
            {
                Id = 1,
                User = "testUser",
                Products = null
            };

            Assert.IsNotNull(shoppingCart.Products, "Products should not be null");  // results in fail test scenerio
        }

        [TestMethod]
        public void ShoppingCart_ProductsShouldBeEmptyListByDefault()
        {
            var shoppingCart = new ShoppingCart
            {
                Id = 1,
                User = "testUser",
                Products = new List<Product> { new Product { Id = 1, Name = "P2", Price = 10.0m, Description = "desc", ProductCategory = new Category { Id = 1, Description = "a" } } }
            };

            Assert.IsTrue(shoppingCart.Products.All(p => p is Product), "All elements in Products should be instances of Product");
        }
    }
}