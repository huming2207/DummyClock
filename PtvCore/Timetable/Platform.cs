using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Platform
    {
        [JsonProperty(PropertyName = "realtime_id")]
        public string RealtimeID { get; set; }
        
        [JsonProperty(PropertyName = "stop")]
        public Stop Stop { get; set; }
        
        [JsonProperty(PropertyName = "direction")]
        public Direction Direction { get; set; }
    }
}
