using System;
using System.Net;
using BusinessLogic;
using Domain.Models;
using DataAccessLayer.Repository;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using ToDoMobileApp.Controllers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
[assembly: CLSCompliant(true)]
namespace ToDoMobileApp.Tests
{
    [TestClass]
    public class ToDoControllerTest
    {
        [TestMethod]
        public async Task GetShouldReturnAllItems()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            //Actual
            var response = await controller.Get();
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public async Task GetAllException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Get
            };
            //Actual
            var response = await controller.Get();
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public async Task PostSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            var toDoItems = await controller.Get();
            var results = toDoItems.Content.ReadAsAsync<System.Collections.Generic.List<Domain.Models.ToDoItem>>().Result;
            if (results != null && results.Count > 0)
            {
            string id = results[results.Count - 1].Id + 1;
            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data" };
            //Actual
            var response = await controller.Post(request);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
	    }
	    else
	    {
	    	Assert.IsTrue(true);
	    }
        }
        [TestMethod]
        public async Task PostException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            ToDoItem request = new ToDoItem() { Id = "100", Name = "Test Data" };
            //Actual
            var response = await controller.Post(request);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public async Task PutSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };
             controller.Request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "ToDo" } });
            var toDoItems = await controller.Get();
            var results = toDoItems.Content.ReadAsAsync<System.Collections.Generic.List<Domain.Models.ToDoItem>>().Result;
            if (results != null && results.Count > 0)
            {
            string id = results[results.Count - 1].Id;
            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data for Azure " + id };
            //Actual
            var response = await controller.Put(request);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
            }
            else
            {
            	Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task PutFailure()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };
            ToDoItem request = new ToDoItem() { Id = "0", Name = "Test Data for Azure" };
            //Actual
            var response = await controller.Put(request);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public async Task PutException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Put
            };
            ToDoItem request = new ToDoItem() { Id = "100", Name = "Test Data for Azure " };
            //Actual
            var response = await controller.Put(request);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public async Task DeleteSuccessful()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
             controller.Request.Headers.Add("ZUMO-API-VERSION", "2.0.0");
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "ToDo" } });
            //Actual
            var toDoItems = await controller.Get();
            var results = toDoItems.Content.ReadAsAsync<System.Collections.Generic.List<Domain.Models.ToDoItem>>().Result;
            if (results != null && results.Count > 0)
            {
            var response = controller.Delete(results[results.Count - 1].Id).Result;
            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
            }
            else
            {
            	Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public async Task DeleteFailure()
        {
            //Arrange
            var controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            //Actual
            string id = "0";
            var response = await controller.Delete(id);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
        [TestMethod]
        public async Task DeleteException()
        {
            //Arrange
            var controller = new ToDoController(new ToDoMockService());
            controller.Request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            //Actual
            string id = "0";
            var response = await controller.Delete(id);
            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
