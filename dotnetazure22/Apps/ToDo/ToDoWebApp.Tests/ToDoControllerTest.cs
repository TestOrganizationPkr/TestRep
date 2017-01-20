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

[assembly: CLSCompliant(true)]

namespace ToDoWebApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ToDoControllerTest
    {
        [TestMethod]
        public void TestCreateAccurateData()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        // Act
                        ToDoItem item = new ToDoItem();
                        item.Name = "Test item";

                        var result = controller.Create(item);
                        var data = result.Data.GetType().GetProperty("data");
                        var dataVal = (ToDoItem)data.GetValue(result.Data, null);

                        //Assert
                        Assert.IsTrue(dataVal.Id > 0, "Successfully created, ToDo Id retrived:" + dataVal.Id);
                    }
                }
            }
        }

        [TestMethod]
        public void TestCreateWrongData()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        // Act
                        ToDoItem item = new ToDoItem();
                        item.Name = "";

                        controller.ModelState.AddModelError("test", "test");

                        var result = controller.Create(item);

                        //Assert
                        Assert.IsTrue(result.Data.ToString() == "error", "Since the model is wrong so throwing error.");
                    }
                }
            }
        }

        [TestMethod]
        public void TestDeleteWrongId()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        // Act
                        controller.ModelState.AddModelError("test", "test");

                        var result = controller.Delete(45);

                        //Assert
                        Assert.IsTrue(result.Data.ToString() == "error", "Since the id is not present so throwing error.");
                    }
                }
            }
        }

        [TestMethod]
        public void TestDeleteAccurateId()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        using (var repositoryGet = new ToDoRepository())
                        {
                            using (var businessLogicGet = new ToDoBL(repositoryGet,new DataCache()))
                            {
                                using (ToDoController controllerGet = new ToDoController(businessLogicGet))
                                {
                                    // Act
                                    var result = controllerGet.GetAll();
                                    var data = result.Data.GetType().GetProperty("data");
                                    var dataVal = (List<ToDoItem>)data.GetValue(result.Data, null);
                                    if (dataVal != null && dataVal.Count > 0)
                                    {
                                    var item = dataVal[dataVal.Count - 1];

                                    var resultDelete = controller.Delete(item.Id);

                                    //Assert
                                    Assert.IsTrue(String.IsNullOrEmpty(resultDelete.Data.ToString()), "Since the id is not present so throwing error.");
                                    }
                                    else
                                    {
                                        Assert.IsTrue(true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestUpdateAccurateData()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        using (var repositoryGet = new ToDoRepository())
                        {
                            using (var businessLogicGet = new ToDoBL(repositoryGet,new DataCache()))
                            {
                                using (ToDoController controllerGet = new ToDoController(businessLogicGet))
                                {
                                    // Act
                                    var result = controllerGet.GetAll();
                                    var data = result.Data.GetType().GetProperty("data");
                                    var dataVal = (List<ToDoItem>)data.GetValue(result.Data, null);
                                    if (dataVal != null && dataVal.Count > 0)
                                    {
                                        var item = dataVal[dataVal.Count - 1];
                                        item.Name = "test update";

                                        var resultUpdate = controller.Update(item);

                                        //Assert
                                        Assert.IsTrue(resultUpdate.Data != null, "Successfully updated");
                                    }
                                    else
                                    {
                                        Assert.IsTrue(true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestUpdateWrongData()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        using (var repositoryGet = new ToDoRepository())
                        {
                            using (var businessLogicGet = new ToDoBL(repositoryGet,new DataCache()))
                            {
                                using (ToDoController controllerGet = new ToDoController(businessLogicGet))
                                {
                                    // Act
                                    var result = controllerGet.GetAll();
                                    var data = result.Data.GetType().GetProperty("data");
                                    var dataVal = (List<ToDoItem>)data.GetValue(result.Data, null);
                                    if (dataVal != null && dataVal.Count > 0)
                                    {
                                    var item = dataVal[dataVal.Count - 1];
                                    item.Name = "";
                                    controller.ModelState.AddModelError("test", "test");
                                    var resultUpdate = controller.Update(item);

                                    //Assert
                                    Assert.IsTrue(resultUpdate.Data.ToString() == "error", "Validation failed so error occur.");
                                    }
                                    else
                                    {
                                        Assert.IsTrue(true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void TestToDoIndex()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository,new DataCache()))
                {
                    using (ToDoController controller = new ToDoController(businessLogic))
                    {
                        // Act
                        ViewResult result = controller.Index() as ViewResult;

                        // Assert
                        Assert.IsNotNull(result);
                    }
                }
            }
        }

        [TestMethod]
        public void TestError()
        {
            // Arrange
            using (ErrorController controller = new ErrorController())
            {
                // Act
                ViewResult result = controller.Index() as ViewResult;

                // Assert
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void TestWarningMessage()
        {
            try
            {
                // Arrange
                Logger.Warning("Test warning message");

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
                throw;
            }
        }

        [TestMethod]
        public void TestToDoCreateException()
        {
            using (var mock = new ToDoMockService())
            {
                // Arrange
                using (ToDoController controller = new ToDoController(mock))
                {
                    // Act
                    ToDoItem item = new ToDoItem();
                    item.Name = "Test data";

                    var result = controller.Create(item);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestToDoDeleteException()
        {
            // Arrange
            using (var mock = new ToDoMockService())
            {
                using (ToDoController controller = new ToDoController(mock))
                {
                    //Act
                    var result = controller.Delete(45);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestToDoGetAllException()
        {
            // Arrange
            using (var mock = new ToDoMockService())
            {
                using (ToDoController controller = new ToDoController(mock))
                {
                    //Act
                    var result = controller.GetAll();

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestToDoUpdateException()
        {
            // Arrange
            using (var mock = new ToDoMockService())
            {
                using (ToDoController controller = new ToDoController(mock))
                {
                    // Act
                    ToDoItem item = new ToDoItem();
                    item.Id = 10;
                    item.Name = "Test data";
                    var result = controller.Update(item);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestToDoUpdateFalseCheck()
        {
            // Arrange
            using (var mockData = new ToDoMockDataService())
            {
                using (ToDoController controller = new ToDoController(mockData))
                {
                    // Act
                    ToDoItem item = new ToDoItem();
                    item.Id = 10;
                    item.Name = "Test data";
                    var result = controller.Update(item);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }
    }
}
