using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace VCapsParser
{
    [ExcludeFromCodeCoverage]
    public class Postgre : IVCAPSDatabase
    {
        /// <summary>
        /// This is the BrockerName
        /// </summary>
        public string brokerName
        {
            get
            {
                return "elephantsql";
            }
        }

        /// <summary>
        /// This is the user defined name, this name we are using for identify and filtering the json data
        /// </summary>
        public string udfName
        {
            get
            {
                return "postgre";
            }
        }

        /// <summary>
        /// This is to get the connection string for postgre database based on raw json passed
        /// </summary>
        /// <param name="rawJson"></param>
        /// <returns></returns>
        public string ConstructConnectionString(string rawJson)
        {
            string connectionString = string.Empty;
            JToken vcapObject = JObject.Parse(rawJson);
            connectionString = Convert.ToString(vcapObject["credentials"]["uri"]);
            return connectionString;
        }
    }
}
