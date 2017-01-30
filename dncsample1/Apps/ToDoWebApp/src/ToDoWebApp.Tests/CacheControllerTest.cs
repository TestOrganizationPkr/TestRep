using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoWebApp.Controllers;
using Xunit;

namespace ToDoWebApp.Tests
{
    public class CacheControllerTest
    {
        //public CacheControllerTest()
        //{
        //    if (_contextOptions == null)
        //    {
        //        // Create a fresh service provider, and therefore a fresh 
        //        // InMemory database instance.
        //        var serviceProvider = new ServiceCollection()
        //            .AddEntityFrameworkInMemoryDatabase()
        //            .BuildServiceProvider();

        //        // Create a new options instance telling the context to use an
        //        // InMemory database and the new service provider.

        //        var builder = new DbContextOptionsBuilder<SQLContext>();

        //        builder.UseInMemoryDatabase("PostgresSQLContext")
        //               .UseInternalServiceProvider(serviceProvider);

        //        _contextOptions = builder.Options;
        //    }
        //}

        [Fact]
        public void TestGetItemCount()
        {
            // All contexts that share the same service provider will share the same InMemory database
            using (CacheController controller = Helpers.CreateCacheControllerObject())
            {
                // Act
                var resultUpdate = (JsonResult)controller.GetItemCount();

                //Assert
                Assert.True(resultUpdate.Value.ToString() != "error", "Successfully get count");
            }
        }

        [Fact]
        public void TestDeleteCache()
        {
            // All contexts that share the same service provider will share the same InMemory database
            using (CacheController controller = Helpers.CreateCacheControllerObject())
            {
                // Act
                var resultDelete = (JsonResult)controller.DeleteCache();

                //Assert
                Assert.True(string.IsNullOrEmpty(resultDelete.Value.ToString()), "Deleted successfully");
            }
        }

        [Fact]
        public void TestGetItemCountException()
        {
            using (CacheController controller = Helpers.CreateCacheMockServiceObjectCache())
            {
                // Act
                var result = (JsonResult)controller.GetItemCount();

                //Assert
                Assert.True(result.Value.ToString() == "error", "Exception occur so error.");
            }

        }

        [Fact]
        public void TestToDoDeleteCacheException()
        {
            using (CacheController controller = Helpers.CreateCacheMockServiceObjectCache())
            {
                //Act
                var result = (JsonResult)controller.DeleteCache();

                //Assert
                Assert.True(result.Value.ToString() == "error", "Exception occur so error.");
            }
        }

        public void CheckCacheCount()
        {
            int CacheCount = 0;
            #region GetcacheCount
            //Arrange
            // All contexts that share the same service provider will share the same InMemory database
            using (CacheController controller = Helpers.CreateCacheControllerObject())
            {
                //Actual
                var response = (JsonResult)controller.GetItemCount();
                int.TryParse(response.Value.ToString(), out CacheCount);
            }

            #endregion

            #region Add item
            //Arrange
            // All contexts that share the same service provider will share the same InMemory database
            using (ToDoController controllerAdd = Helpers.CreateToDoControllerObject())
            {
                string id = "100";
                Domain.Models.ToDoItem request = new Domain.Models.ToDoItem() { Id = id, Name = "Test Data" };

                //Actual
                var responseAdd = controllerAdd.Create(request);
            }

            #endregion


            #region Validate Count
            //Arrange
            using (CacheController controllerValidate = Helpers.CreateCacheControllerObject())
            {
                //Actual
                var responseValidate = (JsonResult)controllerValidate.GetItemCount();
                int CacheCountAfterInsert = 0;
                int.TryParse(responseValidate.Value.ToString(), out CacheCountAfterInsert);

                Assert.Equal((CacheCount + 1), CacheCountAfterInsert);
            }
            #endregion
        }


    }
}
