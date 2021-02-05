using Mine.Models;
using NUnit.Framework;

namespace UnitTests.Models
{
    [TestFixture]
    public class HomeMenueItemTests
    {
        [Test]
        public void HomeMenueItem_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new HomeMenuItem();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void HomeMenueItem_Set_Get_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new HomeMenuItem();
            result.Id = MenuItemType.Game;
            result.Title = "Title";

            // Reset

            // Assert 
            Assert.AreEqual(MenuItemType.Game, result.Id);
            Assert.AreEqual("Title", result.Title);
        }
    }
}
