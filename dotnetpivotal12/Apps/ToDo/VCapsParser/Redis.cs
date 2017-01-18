using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace VCapsParser
{
    [ExcludeFromCodeCoverage]
    public class Redis : IVCAPSDatabase
    {
        /// <summary>
        /// This is the BrockerName
        /// </summary>
        public string brokerName
        {
            get
            {
                return "p-redis";
            }
        }

        /// <summary>
        /// This is the user defined name, this name we are using for identify and filtering the json data
        /// </summary>
        public string udfName
        {
            get
            {
                return "redis";
            }
        }

        /// <summary>
        /// This is to get the connection string for redis database based on raw json passed
        /// </summary>
        /// <param name="rawJson"></param>
        /// <returns></returns>
        public string ConstructConnectionString(string rawJson)
        {
            string connectionString = string.Empty;
            JToken vcapObject = JObject.Parse(rawJson);
            connectionString = string.Format("{0}:{1},password={2}",
                              Convert.ToString(vcapObject["credentials"]["host"]), Convert.ToString(vcapObject["credentials"]["port"]), Convert.ToString(vcapObject["credentials"]["password"]));
            return connectionString;
        }
    }
}
