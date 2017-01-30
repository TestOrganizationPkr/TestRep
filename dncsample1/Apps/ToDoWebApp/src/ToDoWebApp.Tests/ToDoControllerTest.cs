using Xunit;
using ToDOList.Controllers;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DataAccessLayer;

namespace ToDoWebApp.Tests
{
    public class ToDoWebAppDocumentDBTests
    {
        [Fact]
        public void TestGetAll()
        {
            var controller = Helpers.CreateToDoControllerObject();
            var result = controller.GetAll();
            var jsonResult = (JsonResult)result;
            var data = jsonResult.Value.GetType().GetProperty("data");
            var dataVal = (List<ToDoItem>)data.GetValue(jsonResult.Value, null);
            Assert.NotNull(dataVal);
            Assert.True(dataVal.Count >= 0);
        }

        [Fact]
        public void TestGetAllException()
        {
            var controller = Helpers.CreateToDoMockServiceObject();
            var response = controller.GetAll();
            var jsonResult = (JsonResult)response;
            Assert.NotNull(jsonResult);
            Assert.Equal(jsonResult.Value, "error");
        }

        [Fact]
        public void CreateCorrectData()
        {
            var controller = Helpers.CreateToDoControllerObject();
            var item = new ToDoItem { Name = "Insert from Test case" };
            var response = controller.Create(item);
            var jsonResult = (JsonResult)response;
            var data = jsonResult.Value.GetType().GetProperty("data");
            var dataVal = (ToDoItem)data.GetValue(jsonResult.Value, null);
            Assert.NotNull(jsonResult);
            Assert.True(!string.IsNullOrEmpty(dataVal.Id));
        }

        [Fact]
        public void CreateModelError()
        {
            var controller = Helpers.CreateToDoControllerObject();
            controller.ModelState.AddModelError("test", "test");
            var item = new ToDoItem { Name = "Test data from Create invalid Model" };
            var response = controller.Create(item);
            var jsonResult = (JsonResult)response;
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void CreateException()
        {
            var controller = Helpers.CreateToDoMockServiceObject();
            var item = new ToDoItem { Name = "Create Test data from Exception Block" };
            var response = controller.Create(item);
            var jsonResult = (JsonResult)response;
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void UpdateCorrectData()
        {
            JsonResult jsonResult = null;
            var updateRecord = Helpers.GetUpdatedRecords();
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                var response = controller.Update(updateRecord);
                jsonResult = (JsonResult)response;
            }
            var data = jsonResult.Value.GetType().GetProperty("data");
            var dataVal = (ToDoItem)data.GetValue(jsonResult.Value, null);

            Assert.NotNull(dataVal);
            Assert.Equal("Update from Unit Testing", dataVal.Name);
        }

        [Fact]
        public void UpdateModelError()
        {
            JsonResult jsonResult = null;
            var updateRecord = Helpers.GetUpdatedRecords();
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                controller.ModelState.AddModelError("error", "error");
                var response = controller.Update(updateRecord);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void TestUpdateException()
        {
            JsonResult jsonResult = null;
            var updateRecord = Helpers.GetUpdatedRecords();
            using (var controller = Helpers.CreateToDoMockServiceObject())
            {
                var response = controller.Update(updateRecord);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void UpdateIncorrectData()
        {
            JsonResult jsonResult = null;
            var updateRecord = Helpers.GetUpdatedRecords();
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                updateRecord.Id = updateRecord.Id + "1234"; //Appended invalid ID
                var response = controller.Update(updateRecord);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void DeleteCorrectData()
        {
            var records = Helpers.GetRecords();
            JsonResult jsonResult = null;
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                var response = controller.Delete(records.Id);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal(jsonResult.Value, "");
        }

        [Fact]
        public void DeleteIncorrectData()
        {
            var record = Helpers.GetRecords();
            JsonResult jsonResult = null;
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                var wrongId = record.Id + "5";
                var response = controller.Delete(wrongId);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void TestDeleteException()
        {
            var record = Helpers.GetRecords();
            JsonResult jsonResult = null;
            using (var controller = Helpers.CreateToDoMockServiceObject())
            {
                var response = controller.Delete(record.Id);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void TestDeleteNullId()
        {
            JsonResult jsonResult = null;
            var record = Helpers.GetRecords();
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                record.Id = null;
                var response = controller.Delete(record.Id);
                jsonResult = (JsonResult)response;
            }
            Assert.NotNull(jsonResult);
            Assert.Equal("error", jsonResult.Value);
        }

        [Fact]
        public void TestError()
        {
            using (ErrorController controller = new ErrorController())
            {
                ViewResult result = controller.Index() as ViewResult;
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void TestIndex()
        {
            using (var controller = Helpers.CreateToDoControllerObject())
            {
                var result = controller.Index();
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void TestMiscellaneous()
        {
            var todoList = new Models.ToDoList();

            TestHostingEnviroment host = new TestHostingEnviroment();
            Startup str = new Startup(host);
            Assert.NotNull(str);

            TestServiceCollection tstser = new TestServiceCollection();
            TestConfigurationRoot tstcon = new TestConfigurationRoot();
            Program program = new Program();
            Assert.NotNull(program);

            TestHostingEnviroment env = new TestHostingEnviroment();
            Startup s = new Startup(env);
            Assert.NotNull(s.Configuration);
            TestServiceCollection sercoll = new TestServiceCollection();
            s.ConfigureServices(sercoll);
            TestApplicationBuilder appBuilder = new TestApplicationBuilder();
            TestLoggerFactory log = new TestLoggerFactory();
            s.Configure(appBuilder, env, log);
        }
    }
}

