using BusinessLogic;
using Cache;
using DataAccessLayer.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using ToDoWebApp.Controllers;
using System.Threading.Tasks;

[assembly: CLSCompliant(true)]

namespace ToDoWebApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ToDoControllerTest
    {
        [TestMethod]
        public async Task TestCreateAccurateData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            // Act
            ToDoItem item = new ToDoItem();
            item.Name = "Test item";

            var result = await controller.Create(item);
            var data = result.Data.GetType().GetProperty("data");
            var dataVal = (ToDoItem)data.GetValue(result.Data, null);
            
            //Assert
            Assert.IsTrue(dataVal.Id != null, "Successfully created, ToDo Id retrived:" + dataVal.Id);
            
        }

        [TestMethod]
        public async Task TestCreateWrongData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            // Act
            ToDoItem item = new ToDoItem();
            item.Name = "";

            controller.ModelState.AddModelError("test", "test");

            var result = await controller.Create(item);
           
            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Since the model is wrong so throwing error.");
        }

        [TestMethod]
        public async Task TestDeleteWrongId()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository() ,new DataCache()));

            // Act
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Delete("100");

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Since the id is not present so throwing error.");
        }

        [TestMethod]
        public async Task TestDeleteAccurateId()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));
            ToDoController controllerGet = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            // Act
            var result = await controllerGet.GetAll();
            var data = result.Data.GetType().GetProperty("data");
            var dataVal = (List<ToDoItem>) data.GetValue(result.Data, null);
            if (dataVal != null && dataVal.Count > 0)
            {
            var item = dataVal[dataVal.Count - 1];


            var resultDelete = await controller.Delete(item.Id);

            //Assert
             Assert.IsTrue(resultDelete.Data.ToString() == "", "Since the id is not present so throwing error.");
            }
            else{
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task TestUpdateAccurateData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));
            ToDoController controllerGet = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            // Act
            var result = await controllerGet.GetAll();
            var data = result.Data.GetType().GetProperty("data");
            var dataVal = (List<ToDoItem>)data.GetValue(result.Data, null);
            if (dataVal != null && dataVal.Count > 0)
            {
            var item = dataVal[dataVal.Count - 1];
            item.Name = "test update";

            var resultUpdate = await controller.Update(item);

            //Assert
            Assert.IsTrue(resultUpdate.Data != null, "Successfully updated");
            }
            else{
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public async Task TestUpdateWrongData()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));
            ToDoController controllerGet = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            // Act
            var result = await controllerGet.GetAll();
            var data = result.Data.GetType().GetProperty("data");
            var dataVal = (List<ToDoItem>)data.GetValue(result.Data, null);
            if (dataVal != null && dataVal.Count > 0)
            {
            var item = dataVal[dataVal.Count - 1];
            item.Name = "";
            controller.ModelState.AddModelError("test", "test");
            var resultUpdate = await controller.Update(item);

            //Assert
            Assert.IsTrue(resultUpdate.Data.ToString().ToLower() == "error", "Validation failed so error occur.");
            }
            else{
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestToDoIndex()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoBL(new ToDoRepository(),new DataCache()));

            //// Act
            ViewResult result = controller.Index() as ViewResult;

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
        public void TestWarningMessage()
        {
            try
            {
                // Arrange
                Logger.Information("Test warning message");

                // Assert
                Assert.IsTrue(true);
            }
            catch
            {
                // Assert
                Assert.IsTrue(false);
                throw;
            }
        }

        [TestMethod]
        public void TestUnityWebActivatorStart()
        {
            try
            {
                // Arrange
                UnityWebActivator.Start();

                // Assert
                Assert.IsTrue(true);
            }
            catch
            {
                // Assert
                Assert.IsTrue(false);
                throw;
            }
        }

        [TestMethod]
        public void TestUnityWebActivatorShutdown()
        {
            try
            {
                // Arrange
                UnityWebActivator.Shutdown();

                // Assert
                Assert.IsTrue(true);
            }
            catch
            {
                // Assert
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
                // Assert
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
                // Assert
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public async Task TestToDoCreateException()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockService());

            // Act
            ToDoItem item = new ToDoItem();
            item.Name = "Test data";
            
            var result = await controller.Create(item);

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Exception occur so error.");
        }

        [TestMethod]
        public async Task TestToDoDeleteException()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockService());

            //Act
            var result = await controller.Delete("45");

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Exception occur so error.");
        }

        [TestMethod]
        public async Task TestToDoGetAllException()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockService());

            //Act
            var result = await controller.GetAll();

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Exception occur so error.");
        }

        [TestMethod]
        public async Task TestToDoUpdateException()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockService());

            // Act
            ToDoItem item = new ToDoItem();
            item.Id = "10";
            item.Name = "Test data";
            var result = await controller.Update(item);

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Exception occur so error.");
        }

        [TestMethod]
        public async Task TestToDoUpdateFalseCheck()
        {
            // Arrange
            ToDoController controller = new ToDoController(new ToDoMockDataService());

            // Act
            ToDoItem item = new ToDoItem();
            item.Id = "10";
            item.Name = "Test data";
            var result = await controller.Update(item);

            //Assert
            Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Exception occur so error.");
        }


    }
}
