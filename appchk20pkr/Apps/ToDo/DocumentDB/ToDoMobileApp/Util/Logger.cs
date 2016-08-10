using System.Diagnostics;

namespace ToDoMobileApp.Util
{
    /// <summary>
    /// If you want to enable the logging then login to portal.azure.com and navigate to your website.
    /// Then click on “all settings” -> then click on “Diagnostics logs” inside Features.
    /// Then based on your requirement you can either choose Filesystem or Blob, also you select the level of logging as per your requirement. 
    /// After configuring the logging click “Save” on top of the panel.
    /// To view logging go to http://{websitename}.scm.azurewebsites.net/
    /// Click on debug console -> CMD -> LogFiles
    /// </summary>
    public static class Logger
    {
        public static void Information(string logStatement)
        {
            Trace.TraceInformation(logStatement);
        }
        public static void Error(string logStatement)
        {
            Trace.TraceError(logStatement);
        }
		public static void Warning(string logStatement)
        {
            Trace.TraceWarning(logStatement);
        }
    }
}