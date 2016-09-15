using BusinessLogic;
using Cache;
using DataAccessLayer.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoMobileApp.Controllers;
[assembly: CLSCompliant(true)]

namespace ToDoMobileApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ToDoControllerTest
    {
        [TestMethod]
        public void GetShouldReturnAllItems()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
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
        public void GetAllException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
            };

            //Actual
            var response = controller.Get();

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [TestMethod]
        public void PostSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method= HttpMethod.Post,
            };
            
            int id = 100;

            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data" };

            //Actual
            var response = controller.Post(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

        }

        [TestMethod]
        public void PostException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
            };

            ToDoItem request = new ToDoItem() { Id = 100, Name = "Test Data" };

            //Actual
            var response = controller.Post(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [TestMethod]
        public void PutSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));
            

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
            };
            
            var toDoItems = controller.Get();
            var results = toDoItems.Content.ReadAsAsync<System.Collections.Generic.List<Domain.Models.ToDoItem>>().Result;
            int id = results[results.Count - 1].Id;

            var controllerPut = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controllerPut.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };
            controllerPut.Configuration = new HttpConfiguration();
            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data for Azure " + id };

            //Actual
            var response = controllerPut.Put(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void PutFailure()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                };

                ToDoItem request = new ToDoItem() { Id = 0, Name = "Test Data for Azure" };

                //Actual
                var response = controller.Put(request);

                //Assert
                Assert.IsNotNull(response);
                Assert.IsNull(response.Content);
                Assert.IsFalse(response.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            
        }

        [TestMethod]
        public void PutException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
            };

            ToDoItem request = new ToDoItem() { Id = 100, Name = "Test Data for Azure " };

            //Actual
            var response = controller.Put(request);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [TestMethod]
        public void DeleteSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };

            //Actual
            var toDoItems = controller.Get();
            var results = toDoItems.Content.ReadAsAsync<System.Collections.Generic.List<Domain.Models.ToDoItem>>().Result;

            var controllerDelete = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controllerDelete.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };
            controllerDelete.Configuration = new HttpConfiguration();
            var response = controllerDelete.Delete(results[results.Count - 1].Id);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void DeleteFailure()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };

            //Actual
            int id = 0;
            var response = controller.Delete(id);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);

        }

        [TestMethod]
        public void DeleteException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());

            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
            };

            //Actual
            int id = 0;
            var response = controller.Delete(id);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);

        }


    }
}
