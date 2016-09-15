using BusinessLogic;
using Cache;
using DataAccessLayer.Repository;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using ToDoWebApp.Controllers;

namespace ToDoWebApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CacheControllerTest
    {

        [TestMethod]
        public void TestGetItemCount()
        {
            // Arrange
            CacheController controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            // Act

            var resultUpdate = controller.GetItemCount();

            //Assert
            Assert.IsTrue(resultUpdate.Data.ToString() != "error", "Successfully get count");
        }
        
        [TestMethod]
        public void TestDeleteCache()
        {
            // Arrange
            CacheController controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));

            // Act
            var resultDelete = controller.DeleteCache();

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(resultDelete.Data.ToString()), "Deleted successfully");
        }

        [TestMethod]
        public void TestGetItemCountException()
        {
            // Arrange
            CacheController controller = new CacheController(new ToDoMockService());

            // Act

            var result = controller.GetItemCount();

            //Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
        }


        [TestMethod]
        public void TestToDoDeleteCacheException()
        {
            // Arrange
            CacheController controller = new CacheController(new ToDoMockService());

            //Act
            var result = controller.DeleteCache();

            //Assert
            Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
        }

        [TestMethod]
        public void CheckCacheCount()
        {
            int CacheCount = 0;
            #region GetcacheCount
            //Arrange
            var controller = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));
            
            //Actual
            var response = controller.GetItemCount();
            int.TryParse(response.Data.ToString(), out CacheCount);
            #endregion


            #region Add item
            //Arrange
            var controllerAdd = new ToDoController(new ToDoBL(new ToDoRepository(), new DataCache()));
            
            string id = "100";
            ToDoItem request = new ToDoItem() { Id = id, Name = "Test Data" };

            //Actual
            var responseAdd = controllerAdd.Create(request);
            #endregion


            #region Validate Count
            //Arrange
            var controllerValidate = new CacheController(new ToDoBL(new ToDoRepository(), new DataCache()));
            
            //Actual
            var responseValidate = controllerValidate.GetItemCount();
            int CacheCountAfterInsert = 0;
            int.TryParse(responseValidate.Data.ToString(), out CacheCountAfterInsert);

            Assert.AreEqual((CacheCount + 1), CacheCountAfterInsert);
            #endregion

        }
    }
}
