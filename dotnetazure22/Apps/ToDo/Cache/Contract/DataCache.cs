﻿using StackExchange.Redis;
using System;
using System.Configuration;

[assembly: CLSCompliant(true)]

namespace Cache
{
    public class DataCache : IDataCache
    {
        private readonly IDatabase _cache;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        /// <summary>
        /// Here we are initializing the redis connection
        /// </summary>
        public DataCache()
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["Redis"].ConnectionString);
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
