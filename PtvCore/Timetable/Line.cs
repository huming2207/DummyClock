using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Line : Item
    {
        [JsonProperty(PropertyName = "transport_type")]
        public TransportType TransportType { get; set; }

        [JsonProperty(PropertyName = "route_type")]
        public RouteType RouteType { get; set; }

        [JsonProperty(PropertyName = "line_id")]
        public string LineID { get; set; }

        [JsonProperty(PropertyName = "line_name")]
        public string LineName { get; set; }

        [JsonProperty(PropertyName = "line_number")]
        public string LineNumber { get; set; }

        [JsonProperty(PropertyName = "line_number_long")]
        public string LongLineNumber { get; set; }
    }
}
