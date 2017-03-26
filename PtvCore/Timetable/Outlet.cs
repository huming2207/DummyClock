using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Outlet : Location
    {
        [JsonProperty(PropertyName = "outlet_type")]
        public OutletType Type { get; set; }

        [JsonProperty(PropertyName = "route_type")]
        public RouteType RouteType { get; set; }

        [JsonProperty(PropertyName = "business_name")]
        public string BusinessName { get; set; }
    }
}
