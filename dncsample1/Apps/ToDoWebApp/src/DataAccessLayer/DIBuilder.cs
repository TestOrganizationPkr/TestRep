using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class DIBuilder
    {
        public static void AddDepenDency(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddScoped<IToDoRepository, ToDoRepository>();

            services.AddOptions();
            // Section Name from appsettings.json file
            services.Configure<ConfigurationSettings>(config.GetSection("DocumentDBKeys"));

            services.AddSingleton<IConfiguration>(config);
        }
    }
}
