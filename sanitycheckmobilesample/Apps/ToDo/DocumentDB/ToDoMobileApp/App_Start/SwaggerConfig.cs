using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Swagger;
using Swashbuckle.Application;
using System.Web.Http.Description;
using ToDoMobileApp.Util;

namespace ToDoMobileApp
{
    /// <summary>
    /// The Microsoft.Azure.Mobile.Server.Swagger uses Swagger and Swashbuckle to add documentation 
    /// and API Explorer capability to the Mobile App.
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// To register the swagger with the mobile application. 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
           
            if (config != null)
            {
                // Use the custom ApiExplorer that applies constraints. This prevents
                // duplicate routes on /api and /tables from showing in the Swagger doc.
                config.Services.Replace(typeof(IApiExplorer), new MobileAppApiExplorer(config));

                config
                     .EnableSwagger(c =>
                        {
                            c.SingleApiVersion("v1", "ToDoMobileApp");
                            c.IncludeXmlComments(string.Format(@"{0}\bin\ToDoMobileApp.XML",
                               System.AppDomain.CurrentDomain.BaseDirectory));

                        // Tells the Swagger doc that any MobileAppController needs a
                        // ZUMO-API-VERSION header with default 2.0.0
                        c.OperationFilter<MobileAppHeaderFilter>();

                        //This is to remove the NotificationInstallations from default route
                        c.DocumentFilter<SwaggerDocumentationFilter>();
                        })
                    .EnableSwaggerUi();
            }
        }
    }
}
