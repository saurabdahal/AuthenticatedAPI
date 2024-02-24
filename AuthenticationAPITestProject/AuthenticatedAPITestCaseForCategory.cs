using AuthentationWebAPI.Data;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForCategory
    {

        [TestMethod]
        public void Category_ShouldSetPropertiesCorrectly()
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
        public void Category_IdShouldBeAnInteger()
        {
            var category = new Category
            {
                Id = 1,
                Description = "Veggies"
            };

            Assert.IsInstanceOfType(category.Id, typeof(int), "Id should be an instance of int");
        }

        [TestMethod]
        public void Category_DescriptionShouldBeAString()
        {
            var category = new Category
            {
                Id = 1,
                Description = "Lorem Ipsum is overrated"
            };

            Assert.IsInstanceOfType(category.Description, typeof(string), "Description should be an instance of string");
        }

        [TestMethod]
        public void Category_DescriptionShouldNotBeNull()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Description = null
            };

            Assert.IsNotNull(category.Description, "Description should not be null");  // this will generate a fail test scenerio 
        }
    }

       
}