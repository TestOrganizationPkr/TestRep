using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VCapsParser
{
    [ExcludeFromCodeCoverage]
    public static class VCapsEnvParser
    {
        /// <summary>
        /// This is to get the connection string from environment based on ServiceType passed.
        /// If it will not find the value in the environment then it will return empty string
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns>connection string</returns>
        public static string GetConnectionString(ServiceType serviceType)
        {
            string connectionString = string.Empty;
            IVCAPSDatabase ivdb = VCAPSFactory.GetInstance(serviceType);
            string brokerName = ivdb.brokerName;
            string udfName = ivdb.udfName;
            var rawJson = GetRawData(brokerName, "", true);
            if (string.IsNullOrEmpty(rawJson))
            {
                rawJson = GetRawData("user-provided", udfName);
            }
            if (!string.IsNullOrEmpty(rawJson))
            {
                connectionString = ivdb.ConstructConnectionString(rawJson);
            }
            return connectionString;
        }

        /// <summary>
        /// This is to get the raw json from the environment based in attributeName, subattribute or to get only first record.
        /// If subattribute is passed then it will serach for that subattribute inside the attribute json and then return that record
        /// If isFirstRecord is true then it will retrun the first element form the array of attribute element.
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="subattribute"></param>
        /// <param name="isFirstRecord"></param>
        /// <returns></returns>
        public static string GetRawData(string attributeName,string subattribute = "", bool isFirstRecord = false)
        {
            string rawString = string.Empty;
            var strVcapServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            if (!String.IsNullOrEmpty(strVcapServices))
            {
                JToken vcapObject = JObject.Parse(strVcapServices);
                if (vcapObject[attributeName] != null )
                {
                    if (!string.IsNullOrEmpty(subattribute))
                    {
                        for (int counter = 0; counter < vcapObject[attributeName].Count(); counter++)
                        {
                            if (Convert.ToString(vcapObject[attributeName][counter]["name"]).Contains(subattribute))
                            {
                                rawString = Convert.ToString(vcapObject[attributeName][counter]);
                                break;
                            }
                        }
                    }
                    else if (isFirstRecord && vcapObject[attributeName].Count() > 0)
                    {
                        rawString = Convert.ToString(vcapObject[attributeName][0]);
                    }
                    else
                    {
                        rawString = Convert.ToString(vcapObject[attributeName]);
                    } 
                }
            }
            return rawString;
        }
    }
}
