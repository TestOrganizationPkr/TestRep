using BusinessLogic;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using ToDoWebApp.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace ToDoWebApp.Tests
{
    public class Helpers
    {
        public static ToDoController CreateToDoControllerObject()
        {
            // Testing Purpose the values are hardcoded
            var getConfigValues = Options.Create<ConfigurationSettings>(new ConfigurationSettings());

            getConfigValues.Value.DatabaseName = "";
            getConfigValues.Value.AuthKey = "";
            getConfigValues.Value.CollectionName = "";
            getConfigValues.Value.EndPoint = "";

            return new ToDoController(new ToDoBL(new ToDoRepository(getConfigValues),new Cache.DataCache()), new LoggerFactory());
        }

        public static ToDoController CreateToDoMockServiceObject()
        {
            return new ToDoController(new ToDoMockService(), new LoggerFactory());
        }

        public static Domain.Models.ToDoItem GetRecords()
        {
            using (var getController = CreateToDoControllerObject())
            {
                var result = getController.GetAll();
                var jsonResult = (JsonResult)result;
                var data = jsonResult.Value.GetType().GetProperty("data");
                var dataVal = (List<Domain.Models.ToDoItem>)data.GetValue(jsonResult.Value, null);
                return dataVal[dataVal.Count - 1];
            } 
        }

        public static Domain.Models.ToDoItem GetUpdatedRecords()
        {
            var data = GetRecords();
            data.Name = "Update from Unit Testing";
            return data;
        }

        public static void GetConfigurationSettings(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddOptions();

            // Section Name from appsettings.json file
            services.Configure<ConfigurationSettings>(config.GetSection("DocumentDBKeys"));

            services.AddSingleton<IConfiguration>(config);
        }
		
		public static CacheController CreateCacheControllerObject()
        {
            var getConfigValues = Options.Create<ConfigurationSettings>(new ConfigurationSettings());

            getConfigValues.Value.DatabaseName = "";
            getConfigValues.Value.AuthKey = "";
            getConfigValues.Value.CollectionName = "";
            getConfigValues.Value.EndPoint = "";
            return new CacheController(new ToDoBL(new ToDoRepository(getConfigValues), new Cache.DataCache()), new LoggerFactory());
        }

        public static CacheController CreateCacheMockServiceObjectCache()
        {
	        return new CacheController(new ToDoMockService(), new LoggerFactory());
        }
    }
}
