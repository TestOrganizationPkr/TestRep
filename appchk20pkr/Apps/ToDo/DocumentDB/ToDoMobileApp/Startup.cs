using Microsoft.Owin;
using Owin;
using System.Diagnostics.CodeAnalysis;

[assembly: OwinStartup(typeof(ToDoMobileApp.Startup))]

namespace ToDoMobileApp
{
    [ExcludeFromCodeCoverage]
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}