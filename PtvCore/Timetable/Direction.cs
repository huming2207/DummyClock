using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Direction
    {
        [JsonProperty(PropertyName = "linedir_id")]
        public uint LineDirectionID { get; set; }

        [JsonProperty(PropertyName = "direction_id")]
        public uint DirectionID { get; set; }
        
        [JsonProperty(PropertyName = "direction_name")]
        public string DirectionName { get; set; }

        [JsonProperty(PropertyName = "line")]
        public Line Line { get; set; }
    }
}
