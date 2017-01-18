using BusinessLogic;
using Cache;
using DataAccessLayer.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;
using ToDoWebApp.Controllers;
[assembly: CLSCompliant(true)]
namespace ToDoWebApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ToDoControllerTest
    {
        private MockFactory _factory;
        private Mock<IToDoRepository> mock;
        private Mock<IDataCache> mockCache;
       
        public ToDoControllerTest()
        {
            _factory = new MockFactory();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            mock = _factory.CreateMock<IToDoRepository>();
            mockCache = _factory.CreateMock<IDataCache>();
            mock.Expects.One.Method(_ => _.Dispose());
        }

        [TestMethod]
        public void TestMock()
        {
            //Assert that the mock is not null
            Assert.IsNotNull(mock, "mock was null");

            //Assert that the mock is not of the interface type
            Assert.IsFalse(typeof(IToDoRepository).IsInstanceOfType(mock), "mock is not an IToDoRepository, it is a Mock<IToDoRepository>");
        }

        [TestMethod]
        public void TestGetAllWithData()
        {
            var list = new List<DataAccessLayer.Models.ToDoItem>();
            list.Add(new DataAccessLayer.Models.ToDoItem { Id = 6, Name = "sampledata" });
            mock.Expects.One.Method(_ => _.All()).WillReturn(list.AsQueryable());
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetAllWithException()
        {
            mock.Expects.One.Method(_ => _.All()).Will(Throw.Exception(new Exception()));
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.GetAll();

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception thrown");
        }

        [TestMethod]
        public void TestCreateAccurateData()
        {
            mock.Expects.One.Method(_ => _.Create(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WithAnyArguments().WillReturn(new DataAccessLayer.Models.ToDoItem { Id = 6, Name = "sampledata" });
            mockCache.Expects.One.Method(_ => _.Increment("itemcount")).WithAnyArguments();
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Create(new ToDoItem { Name = "sample2" });
            var data = result.Data.GetType().GetProperty("data");
            var dataVal = (ToDoItem)data.GetValue(result.Data, null);

            Assert.IsTrue(dataVal.Id > 0, "Successfully created, ToDo Id retrived:" + dataVal.Id);

        }

        [TestMethod]
        public void TestCreateException()
        {
            mock.Expects.One.Method(_ => _.Create(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WithAnyArguments().Will(Throw.Exception(new Exception()));
            mockCache.Expects.One.Method(_ => _.Increment("itemcount")).WithAnyArguments();
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Create(new ToDoItem { Name = "sample2" });

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception thrown");

        }

        [TestMethod]
        public void TestCreateWrongData()
        {
            mock.Expects.One.MethodWith(_ => _.Create(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WillReturn(null);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            controller.ModelState.AddModelError("test", "test");
            var result = controller.Create(new ToDoItem { Name = null });

            //Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Since the model is wrong so throwing error.");

        }
        [TestMethod]
        public void TestDeleteWrongId()
        {
            mock.Expects.One.MethodWith(_ => _.Find(45)).WillReturn(null);
            mock.Expects.One.MethodWith(_ => _.Delete(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WillReturn(0);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Delete(45);

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Since the id is not present so throwing error.");
           
        }
        [TestMethod]
        public void TestDeleteAccurateId()
        {
            mock.Expects.One.Method(_ => _.Find(45)).WithAnyArguments().WillReturn(new DataAccessLayer.Models.ToDoItem { Id = 45, Name = "sample" });
            mock.Expects.One.Method(_ => _.Delete(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WithAnyArguments().WillReturn(1);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Delete(45);

            // Assert
            Assert.IsTrue(true);
           
        }
        [TestMethod]
        public void TestDeleteException()
        {
            mock.Expects.One.Method(_ => _.Find(45)).WithAnyArguments().Will(Throw.Exception(new Exception()));
            mock.Expects.One.Method(_ => _.Delete(new DataAccessLayer.Models.ToDoItem { Name = "sample" })).WithAnyArguments().WillReturn(1);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Delete(45);

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception thrown");

        }
        [TestMethod]
        public void TestUpdateAccurateData()
        {
            mock.Expects.One.Method(_ => _.Update(new DataAccessLayer.Models.ToDoItem { Id = 6, Name = "sample2 updated" })).WithAnyArguments().WillReturn(1);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Update(new ToDoItem { Id = 6, Name = "sample2 updated" });

            //Assert
            Assert.IsTrue(result.Data != null, "Successfully updated");

        }
        [TestMethod]
        public void TestUpdateException()
        {
            mock.Expects.One.Method(_ => _.Update(new DataAccessLayer.Models.ToDoItem { Id = 6, Name = "sample2 updated" })).WithAnyArguments().Will(Throw.Exception(new Exception()));
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Update(new ToDoItem { Id = 6, Name = "sample2 updated" });

            //Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception thrown");

        }
        [TestMethod]
        public void TestUpdateWrongData()
        {
            mock.Expects.One.Method(_ => _.Update(new DataAccessLayer.Models.ToDoItem { Id = 5, Name = "some data" })).WithAnyArguments().WillReturn(0);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Update(new ToDoItem { Id = 5, Name = "some data" });

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Id was not found");

        }

        [TestMethod]
        public void TestUpdateWithoutId()
        {
            mock.Expects.One.Method(_ => _.Update(new DataAccessLayer.Models.ToDoItem { Name = "some data" })).WithAnyArguments().WillReturn(0);
            var controller = new ToDoController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.Update(new ToDoItem { Name = "some data" });

            // Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Id not found");

        }
        [TestMethod]
        public void TestToDoIndex()
        {
            // Arrange
            using (var repository = new ToDoRepository())
            {
                using (var businessLogic = new ToDoBL(repository, mockCache.MockObject))
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
        public void TestDeleteCache()
        {
            mockCache.Expects.One.Method(_ => _.Remove("Itemcount")).WithAnyArguments().WillReturn(true);
            var controller = new CacheController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.DeleteCache();

            // Assert
            Assert.IsTrue(Convert.ToString(result.Data) == "", "Item Count deleted successfully");
        }

        [TestMethod]
        public void TestDeleteExceptionCache()
        {
            mockCache.Expects.One.Method(_ => _.Remove("Itemcount")).WithAnyArguments().Will(Throw.Exception(new Exception()));
            var controller = new CacheController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.DeleteCache();

            // Assert
            Assert.IsTrue(Convert.ToString(result.Data) == "error", "remove exception check");
        }

        [TestMethod]
        public void TestItemCountCache()
        {
            mockCache.Expects.One.Method(_ => _.GetValue("Itemcount")).WithAnyArguments().WillReturn("14");
            var controller = new CacheController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.GetItemCount();

            // Assert
            Assert.IsTrue(Convert.ToString(result.Data) == "14", "Item Count");
        }

        [TestMethod]
        public void TestItemCountExceptionCache()
        {
            mockCache.Expects.One.Method(_ => _.GetValue("Itemcount")).WithAnyArguments().Will(Throw.Exception(new Exception()));
            var controller = new CacheController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.GetItemCount();

            // Assert
            Assert.IsTrue(Convert.ToString(result.Data) == "error", "Item Count exception check");
        }

        [TestMethod]
        public void TestItemCountNullCache()
        {
            mockCache.Expects.One.Method(_ => _.GetValue("Itemcount")).WithAnyArguments().WillReturn(null);
            var controller = new CacheController(new ToDoBL(mock.MockObject, mockCache.MockObject));
            var result = controller.GetItemCount();

            // Assert
            Assert.IsTrue(Convert.ToString(result.Data) == "0", "Item Count");
        }


    }
}
