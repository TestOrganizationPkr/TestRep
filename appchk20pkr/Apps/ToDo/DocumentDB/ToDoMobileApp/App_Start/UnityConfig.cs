using DAL.Repository;
using Microsoft.Practices.Unity;
using Service;
using System.Web.Http;
using Unity.WebApi;

namespace ToDoMobileApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            config.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IToDoService, ToDoService>();
            container.RegisterType<IToDoRepository, ToDoRepository>();
        }
    }
}