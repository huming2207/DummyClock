using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Health
    {
        [JsonProperty(PropertyName = "securityTokenOK")]
        public bool IsSecurityTokenOK { get; set; }

        [JsonProperty(PropertyName = "clientClockOK")]
        public bool IsClientClockOK { get; set; }

        [JsonProperty(PropertyName = "memcacheOK")]
        public bool IsMemcacheOK { get; set; }

        [JsonProperty(PropertyName = "databaseOK")]
        public bool IsDatabaseOK { get; set; }

        [JsonIgnore()]
        public bool IsOK
        {
            get
            {
                return this.IsSecurityTokenOK && this.IsClientClockOK && this.IsMemcacheOK && this.IsDatabaseOK;
            }
        }
    }
}
