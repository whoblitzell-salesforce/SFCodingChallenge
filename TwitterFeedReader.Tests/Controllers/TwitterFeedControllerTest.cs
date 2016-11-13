using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterFeedReader;
using TwitterFeedReader.Controllers;
using TwitterFeedReader.Models;
using System.Threading.Tasks;

namespace TwitterFeedReader.Tests.Controllers
{
    [TestClass]
    public class TwitterFeedControllerTest
    {
        [TestMethod]
        public async Task GetWithNoFilter()
        {
            // Arrange
            TwitterFeedController controller = new TwitterFeedController();
            int testCount = 10;
            string testUser = "Salesforce";

            // Act
            IEnumerable<TwitterFeedModel> result = await controller.Get(testCount, testUser, string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public async Task GetWithFakeFilter()
        {
            // Arrange
            TwitterFeedController controller = new TwitterFeedController();
            int testCount = 10;
            string testUser = "Salesforce";
            string filter = "f00bar123";

            // Act
            IEnumerable<TwitterFeedModel> result = await controller.Get(testCount, testUser, filter);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

    }
}
