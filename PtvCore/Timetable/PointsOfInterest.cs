using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class PointsOfInterest
    {
        [JsonProperty(PropertyName = "minLat")]
        public double MinimumLatitude { get; set; }

        [JsonProperty(PropertyName = "minLong")]
        public double MinimumLongitude { get; set; }

        [JsonProperty(PropertyName = "maxLat")]
        public double MaximumLatitude { get; set; }

        [JsonProperty(PropertyName = "maxLong")]
        public double MaximumLongitude { get; set; }

        [JsonProperty(PropertyName = "weightedLat")]
        public double WeightedLatitude { get; set; }

        [JsonProperty(PropertyName = "weightedLong")]
        public double WeightedLongitude { get; set; }

        [JsonProperty(PropertyName = "totalLocations")]
        public uint TotalLocations { get; set; }

        [JsonProperty(PropertyName = "locations")]
        public Location[] Locations { get; set; }
    }
}
