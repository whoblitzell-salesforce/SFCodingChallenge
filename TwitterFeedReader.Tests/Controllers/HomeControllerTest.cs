using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedReader;
using TwitterFeedReader.Controllers;

namespace TwitterFeedReader.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Twitter Feed Reader Coding Challenge", result.ViewBag.Title);
        }
    }
}
