using AuthentationWebAPI.Data;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForCategory
    {

        [TestMethod]
        public void CategoryModel_ShouldSetPropertiesCorrectly()
        {
            var category = new Category
            {
                Id = 1,
                Description = "Fruit"
            };

            Assert.AreEqual(1, category.Id);
            Assert.AreEqual("Fruit", category.Description);
        }

        [TestMethod]
        public void CategoryModel_IdShouldBeAnInteger()
        {
            var category = new Category
            {
                Id = 1,
                Description = "TestCategory"
            };

            Assert.IsInstanceOfType(category.Id, typeof(int), "Category Id should be an instance of int");
        }

        [TestMethod]
        public void CategoryModel_DescriptionShouldBeAString()
        {
            var category = new Category
            {
                Id = 1,
                Description = "TestCategory"
            };

            Assert.IsInstanceOfType(category.Description, typeof(string), "Category Description should be an instance of string");
        }

        [TestMethod]
        public void CategoryModel_DescriptionShouldNotBeNull()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Description = null
            };

            Assert.IsNotNull(category.Description, "Category Description should not be null");
        }
    }

       
}