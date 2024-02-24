using AuthentationWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForProduct
    {

        [TestMethod]
        public void ProductModel_ShouldSetPropertiesCorrectly()
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
        public void ProductModel_IdShouldBeAnInteger()
        {
            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Price = 10.0m,
                Description = "TestDescription",
                ProductCategory = new Category { Id = 1, Description = "TestCategory" }
            };

            Assert.IsInstanceOfType(product.Id, typeof(int), "Product Id should be an instance of int"); 

        }

        [TestMethod]
        public void ProductModel_PiceShouldBeAnDecimal()
        {
            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                Price = 10.0m,
                Description = "TestDescription",
                ProductCategory = new Category { Id = 1, Description = "TestCategory" }
            };

            Assert.IsInstanceOfType(product.Price, typeof(decimal), "Product Price should be an instance of decimal");

        }

        [TestMethod]
        public void ProductModel_ProductCategoryShouldBeInstanceOfCategory()
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