using AuthentationWebAPI.Data;

namespace AuthenticationAPITestProject
{
    [TestClass]
    public class AuthenticatedAPITestCaseForCategory
    {

        [TestMethod]
        public void CategoryModel_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Description = "TestCategory"
            };

            // Act - No action required for property setting

            // Assert
            Assert.AreEqual(1, category.Id);
            Assert.AreEqual("TestCategory", category.Description);
        }
    }
}