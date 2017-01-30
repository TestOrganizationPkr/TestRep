using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cache
{
    public static class DIBuilder
    {
        /// <summary>
        /// This method registers dependency
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddDepenDency(IServiceCollection services, IConfigurationRoot config)
        {           
            services.AddScoped<IDataCache, DataCache>();
        }
    }
}
