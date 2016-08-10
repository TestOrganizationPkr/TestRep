using BusinessLogic;
using Cache;
using DataAccessLayer.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using ToDoMobileApp.Controllers;

namespace ToDoMobileApp.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CacheControllerTest
    {
        [TestMethod]
        public void GetCache()
        {
            //Arrange
            var controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            
            //Actual
            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetCacheException()
        {
            //Arrange
            var controller = new CacheController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get             
            };
            
            //Actual
            var response = controller.Get();

            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void DeleteCache()
        {
            //Arrange
            var controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };

            //Actual
            var response = controller.Delete();

            //Assert
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [TestMethod]
        public void DeleteCacheException()
        {
            //Arrange
            var controller = new CacheController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            
            //Actual
            var response = controller.Delete();

            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
