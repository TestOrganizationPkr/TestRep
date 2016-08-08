using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
[assembly: CLSCompliant(true)]
namespace ToDoWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// This is invoke when the application start
        /// In this we are register the routes and bundles config
        /// </summary>
        /// 
        [ExcludeFromCodeCoverage]
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }
        /// <summary>
        /// This will handled all the unhandled exception in the application 
        /// In this we are reading the last error occcur and logging that error.
        /// After that we will stop all the running script and after the redirect to the custom error page.
        /// </summary>
        /// 
        [ExcludeFromCodeCoverage]
        protected void Application_Error()
        {
            string filePath = string.Empty, errorMessage = string.Empty;
            //Get exception details
            var exception = Server.GetLastError();
            if (null != exception)
            {
                errorMessage = exception.Message;
                filePath = Context.Request.FilePath;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Server.ClearError();
                Logger.Error("Exception in Application_Error :" + exception.Message + exception.StackTrace);
                Logger.Error("Error in file : " + filePath + " | with ErroMessage: " + errorMessage);
            }
            //Skip IIS related error 
            HttpContext.Current.Response.TrySkipIisCustomErrors = true;
            //This will redirect will to the error page which is inside Error/Index.cshtml page
            HttpContext.Current.Response.Redirect("~/Error");
            //The End method causes the Web server to stop processing the script and return the current result. The remaining contents of the file are not processed.      
            HttpContext.Current.Response.End();
            //The Flush method sends buffered output immediately. This method causes a run-time error if Response.Buffer has not been set to TRUE.
            HttpContext.Current.Response.Flush();
            HttpContext.Current.ClearError();
        }
    }
}
