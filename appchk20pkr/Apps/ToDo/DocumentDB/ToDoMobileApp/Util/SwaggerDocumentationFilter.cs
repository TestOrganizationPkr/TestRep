using Swashbuckle.Swagger;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http.Description;

namespace ToDoMobileApp.Util
{
    /// <summary>
    /// This is to modify the swagger document using IDocumentFilter.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SwaggerDocumentationFilter : IDocumentFilter
    {
        /// <summary>
        /// To remove the NotificationInstallations from default route
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {

            if (swaggerDoc != null)
            {
                //NotificationInstallations are not used in Todo mobile app, so the path is removed from swagger document
                //If we need the notifications comment the below lines and enable the UseDefaultConfiguration in SwaggerConfig class 
                if (swaggerDoc.paths.ContainsKey("/api/NotificationInstallations"))
                    swaggerDoc.paths.Remove("/api/NotificationInstallations");
		if (swaggerDoc.definitions.ContainsKey("NotificationInstallation"))
                    swaggerDoc.definitions.Remove("NotificationInstallation");
                if (swaggerDoc.definitions.ContainsKey("NotificationTemplate"))
                    swaggerDoc.definitions.Remove("NotificationTemplate");
                if (swaggerDoc.definitions.ContainsKey("NotificationSecondaryTile"))
                    swaggerDoc.definitions.Remove("NotificationSecondaryTile");
            }

        }
    }
}