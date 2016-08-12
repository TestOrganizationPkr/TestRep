using BlobStorage;
using DataAccessLayer.Repository;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
namespace BusinessLogic.ServiceLocation
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfigMobileApp
    {
        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            config.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IToDoBL,ToDoBL>();
            container.RegisterType<IToDoRepository,ToDoRepository>();
            container.RegisterType<IBlob, Blob>();
container.RegisterType<IBlobBL, BlobBL>();
        }
    }
}
