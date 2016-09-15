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

        [TestMethod]
        public void CheckCacheCount()
        {
            int CacheCount = 0;
            #region GetcacheCount
            //Arrange
            var controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            
            //Actual
            var response = controller.Get();
            int.TryParse(response.Content.ReadAsStringAsync().Result.ToString(),out CacheCount);
            #endregion


            #region Add item
            //Arrange
            var controllerAdd = new ToDoController(new ToDoBL(new ToDoRepository(), new DataCache()));

            controllerAdd.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            int id = 100;
            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data" };

            //Actual
            var responseAdd = controllerAdd.Post(request);
            #endregion


            #region Validate Count
            //Arrange
            var controllerValidate = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            controllerValidate.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            
            //Actual
            var responseValidate = controllerValidate.Get();
            int CacheCountAfterInsert = 0; 
            int.TryParse(responseValidate.Content.ReadAsStringAsync().Result.ToString(), out CacheCountAfterInsert);

            Assert.AreEqual((CacheCount + 1), CacheCountAfterInsert);
            #endregion

        }
    }
}
