using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment.Services;

namespace Specs
{
    [TestClass]
    public class ApiUnitTest
    {
        string searchType = "koop";
        int pageSize = 150;

        [TestMethod]
        public void CanPerformSearchQuery()
        {
            // Arrange
            var target = new FundaApi();

            // Act
            var result = target.Query(searchType, "/amsterdam/tuin", pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void CanPerformSearchQueryMultiplePages()
        {
            // Arrange
            var target = new FundaApi();

            // Act
            var result = target.Query(searchType, "/amsterdam/tuin", pageSize);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > pageSize);
        }

        [TestMethod]
        public void CanGetTop10ListAmsterdam()
        {
            // Arrange
            var target = new FundaApi();

            // Act
            var result = target.GetTop10(searchType, "/amsterdam");

            // Assert
            Assert.AreNotEqual(result.First().MakelaarId, result.Last().MakelaarId);
        }

        [TestMethod]
        public void CanGetTop10ListAmsterdamWithGarden()
        {
            // Arrange
            var target = new FundaApi();

            // Act
            var result = target.GetTop10(searchType, "/amsterdam/tuin");

            // Assert
            Assert.AreNotEqual(result.First().MakelaarId, result.Last().MakelaarId);
        }
    }
}
