using Newtonsoft.Json.Linq;
using System;

namespace VCapsParser
{
    public class MySql : IVCAPSDatabase
    {
        /// <summary>
        /// This is the BrockerName
        /// </summary>
        public string brokerName
        {
            get
            {
                return "p-mysql";
            }
        }

        /// <summary>
        /// This is the user defined name, this name we are using for identify and filtering the json data
        /// </summary>
        public string udfName
        {
            get
            {
                return "mysql";
            }
        }

        /// <summary>
        /// This is to get the connection string for mysql database based on raw json passed
        /// </summary>
        /// <param name="rawJson"></param>
        /// <returns></returns>
        public string ConstructConnectionString(string rawJson)
        {
            string connectionString = string.Empty;
            JToken vcapObject = JObject.Parse(rawJson);
            connectionString = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
                              Convert.ToString(vcapObject["credentials"]["hostname"]), Convert.ToString(vcapObject["credentials"]["name"]), Convert.ToString(vcapObject["credentials"]["username"]), Convert.ToString(vcapObject["credentials"]["password"]), Convert.ToString(vcapObject["credentials"]["port"]));
            return connectionString;
        }
    }
}
