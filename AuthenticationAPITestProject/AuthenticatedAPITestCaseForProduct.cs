using AuthentationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForProduct
    {

        [TestMethod]
        public void Product_ShouldSetPropertiesCorrectly()
        {
            var product = new Product
            {
                Id = 1,
                Name = "apple",
                Price = 10.0m,
                Description = "Apple product",
                ProductCategory = new Category { Id = 1, Description = "Fruit" }
            };


            Assert.AreEqual(1, product.Id);
            Assert.AreEqual("apple", product.Name);
            Assert.AreEqual(10.0m, product.Price);
            Assert.AreEqual("Apple product", product.Description);
            Assert.IsNotNull(product.ProductCategory);
            Assert.AreEqual(1, product.ProductCategory.Id);
            Assert.AreEqual("Fruit", product.ProductCategory.Description);
        }

        [TestMethod]
        public void Product_IdShouldBeAnInteger()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Barbell",
                Price = 10.0m,
                Description = "Barbell is awesome",
                ProductCategory = new Category { Id = 1, Description = "Gym item" }
            };

            Assert.IsInstanceOfType(product.Id, typeof(int), "Id should be an instance of int"); 

        }

        [TestMethod]
        public void Product_PiceShouldBeAnDecimal()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Banana",
                Price = 10.0m,
                Description = "Fruit",
                ProductCategory = new Category { Id = 1, Description = "Fruit is good" }
            };

            Assert.IsInstanceOfType(product.Price, typeof(decimal), "Price should be an instance of decimal");

        }

        [TestMethod]
        public void Product_ProductCategoryShouldBeInstanceOfCategory()
        {
            var product = new Product
            {
                Id = 1,
                Name = "mango",
                Price = 10.0m,
                Description = "mango is a fruit",
                ProductCategory = new Category { Id = 1, Description = "fruit" }
            };


            Assert.IsInstanceOfType(product.ProductCategory, typeof(Category), "Product Category should be an instance of Category");
            
        }

    }
}