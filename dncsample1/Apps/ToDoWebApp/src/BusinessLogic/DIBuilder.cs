using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class DIBuilder
    {

        /// <summary>
        /// This is to call RegisterRepository of DAL layer        
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        private static void RegisterRepository(IServiceCollection services, IConfigurationRoot config)
        {
            DataAccessLayer.DIBuilder.AddDepenDency(services, config);
            Cache.DIBuilder.AddDepenDency(services, config);
            services.AddScoped<IToDoBL, ToDoBL>();
        }

        /// <summary>
        /// This is to call RegisterRepository  
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        public static void AddDependency(IServiceCollection services, IConfigurationRoot config)
        {
            RegisterRepository(services, config);
        }
    }
}
