using System;
using Domain.Models;
using BusinessLogic;
using System.Web.Mvc;
using ToDoWebApp.Models;
using ToDoWebApp.Controllers;
using DataAccessLayer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
[assembly: CLSCompliant(true)]
namespace ToDoWebApp.Tests
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    public class ToDoControllerTest 
    {        
        [TestMethod]
        public void TestCreateAccurateData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            ToDoItem item = new ToDoItem();
            item.Name = "Test item";
            var result = (RedirectToRouteResult)controller.Create(item).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void TestCreateWrongData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            ToDoItem item = new ToDoItem();
            item.Name = "";
            controller.ModelState.AddModelError("test", "test");
            var result = (RedirectToRouteResult)controller.Create(item).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("Error", result.RouteValues["Controller"]);
        }
        [TestMethod]
        public void TestDeleteWrongId()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            var result = (RedirectToRouteResult)controller.Delete("45").Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("Error", result.RouteValues["Controller"]);
        }
        [TestMethod]
        public void TestDeleteAccurateId()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            ToDoController controllerDelete = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            ViewResult resultGetAll = controllerDelete.Index().Result as ViewResult;
            ToDoList model = (ToDoList)resultGetAll.Model;
            //Please check the below id is correct otherwise this test case fail
            var result = (RedirectToRouteResult)controller.Delete(model.Items[model.Items.Count -1].Id).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void TestUpdateAccurateId()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            ToDoController controllerDelete = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            ViewResult resultGetAll = controllerDelete.Index().Result as ViewResult;
            ToDoList model = (ToDoList)resultGetAll.Model;
            //Please check the below id is correct otherwise this test case fail
            var updateLastRecord = model.Items[model.Items.Count - 1];
            updateLastRecord.Name = "Updated via Unit Testing";
            var result = (RedirectToRouteResult)controller.Update(updateLastRecord).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void TestToDoIndex()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
             ViewResult result = controller.Index().Result as ViewResult;
            // Assert
              Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestError()
        {
            // Arrange
            ErrorController controller = new ErrorController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestUnityWebActivatorStart()
        {
            try
            {
                UnityWebActivator.Start();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
                throw;
            }
        }
        [TestMethod]
        public void TestUnityWebActivatorShutdown()
        {
            try
            {
                UnityWebActivator.Shutdown();
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
                throw;
            }
        }
        [TestMethod]
        public void TestRegisterRoute()
        {
            try
            {
                RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
                throw;
            }
        }
        [TestMethod]
        public void TestBundleTable()
        {
            try
            {
                BundleConfig.RegisterBundles(System.Web.Optimization.BundleTable.Bundles);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
                throw;
            }
        }
        [TestMethod]
        public void TestToDoIndexException()
        {
            // Arrange
            using (ToDoController controller = new ToDoController(new ToDoMockService()))
            {
                // Act
                var result = (RedirectToRouteResult)controller.Index().Result;
                // Assert
                Assert.IsNotNull(result, "Not a redirect result");
                Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
                Assert.AreEqual("Index", result.RouteValues["Action"]);
                Assert.AreEqual("Error", result.RouteValues["Controller"]);
            }
        }
        [TestMethod]
        public void TestToDoCreateException()
        {
            // Arrange
            using (ToDoController controller = new ToDoController(new ToDoMockService()))
            {
                // Act
                ToDoItem item = new ToDoItem();
                item.Name = "Test data";
                var result = (RedirectToRouteResult)controller.Create(item).Result;
                // Assert
                Assert.IsNotNull(result, "Not a redirect result");
                Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
                Assert.AreEqual("Index", result.RouteValues["Action"]);
                Assert.AreEqual("Error", result.RouteValues["Controller"]);
            }
        }
        [TestMethod]
        public void TestToDoDeleteException()
        {
            // Arrange
            using (ToDoController controller = new ToDoController(new ToDoMockService()))
            {
                // Act
                var result = (RedirectToRouteResult)controller.Delete("45").Result;
                // Assert
                Assert.IsNotNull(result, "Not a redirect result");
                Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
                Assert.AreEqual("Index", result.RouteValues["Action"]);
                Assert.AreEqual("Error", result.RouteValues["Controller"]);
            }
        }
        [TestMethod]
        public void TestToDoUpdateException()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockService());
            // Act
            ToDoItem item = new ToDoItem();
            item.Id = "10";
            item.Name = "Test data";
            var result = (RedirectToRouteResult)controller.Update(item).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("Error", result.RouteValues["Controller"]);
        }
        [TestMethod]
        public void TestUpdateWrongData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository()));
            ToDoController controllerGet = new ToDoController(new ToDoBL(new ToDoRepository()));
            // Act
            ViewResult resultGetAll = controllerGet.Index().Result as ViewResult;
            ToDoList model = (ToDoList)resultGetAll.Model;
            Domain.Models.ToDoItem item = model.Items[model.Items.Count - 1];
            item.Name = "";
            controller.ModelState.AddModelError("test", "test");
            //Please check the below id is correct otherwise this test case fail
            var result = (RedirectToRouteResult)controller.Update(item).Result;
            // Assert
            Assert.IsNotNull(result, "Not a redirect result");
            Assert.IsFalse(result.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", result.RouteValues["Action"]);
            Assert.AreEqual("Error", result.RouteValues["Controller"]);
        }
    }
}
