using Microsoft.Extensions.Configuration;
using StackExchange.Redis; 

namespace Cache
{
    public class DataCache : IDataCache
    {
        private readonly IDatabase _cache;
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        IConfiguration Configuration { get; set; }


        /// <summary>
        /// Here we are initializing the redis connection
        /// </summary>
        public DataCache()
        {            
            //There is as issue if we directly used host name as connection to redis 
            //So there is as issue in core 1.0.0 , which is going to resolved in dot net core 1.2 version
            //Below are fews link refer to that issue
            // https://github.com/StackExchange/StackExchange.Redis/issues/454
            //https://github.com/dotnet/corefx/issues/5829#issuecomment-207553272
            //https://github.com/dotnet/corefx/issues/8768
            //Once the new version of core 1.2 is avalible we can use the below code to connect with redis
            //_connectionMultiplexer = ConnectionMultiplexer.Connect("<host>:<port>,password=<password>");
            //Below is the fixed for dotnet core 1.0 version
            var builder = new ConfigurationBuilder()                    
                     .AddJsonFile("appsettings.json")
                     .AddEnvironmentVariables(); 
            Configuration = builder.Build();
            _connectionMultiplexer = ConnectionMultiplexer.Connect(Configuration["Data:RedisConnection"]);
            _cache = _connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// This is to get the value based on key from redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            var value = string.Empty;
            var length = _cache.KeyExists(key);
            if (length)
            {
                value = _cache.StringGet(key);
            }
            return value;
        }

        /// <summary>
        /// This is to remove the item base on key from redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _cache.KeyDelete(key);
        }

        /// <summary>
        /// This is to increment the value
        /// </summary>
        /// <param name="key"></param>
        public void Increment(string key)
        {
            _cache.StringIncrement(key);
        }
    }
}
