using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Run : Item
    {
        [JsonProperty(PropertyName = "transport_type")]
        public TransportType TransportType { get; set; }

        [JsonProperty(PropertyName = "route_type")]
        public RouteType RouteType { get; set; }
        
        [JsonProperty(PropertyName = "run_id")]
        public uint RunID { get; set; }
        
        [JsonProperty(PropertyName = "num_skipped")]
        public uint NumberSkipped { get; set; }
        
        [JsonProperty(PropertyName = "destination_id")]
        public string DestinationID { get; set; }
        
        [JsonProperty(PropertyName = "destination_name")]
        public string DestinationName { get; set; }
    }
}
