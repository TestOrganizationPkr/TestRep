using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ToDoMobileApp.Startup))]

namespace ToDoMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}