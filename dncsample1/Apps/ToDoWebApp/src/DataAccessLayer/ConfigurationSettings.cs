using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ConfigurationSettings
    {
        public string DatabaseName { get; set; }
        public string EndPoint { get; set; }
        public string AuthKey { get; set; }
        public string CollectionName { get; set; }
    }
}
