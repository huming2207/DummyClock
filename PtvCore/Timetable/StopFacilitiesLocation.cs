using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PtvCore.Timetable
{ 
    [JsonObject()]
    public class StopFacilitiesLocation
    {
        [JsonProperty(PropertyName = "suburb")]
        public string Suburb { get; set; }

        [JsonProperty(PropertyName = "post_code")]
        public string PostCode { get; set; }

        [JsonProperty(PropertyName = "municipality")]
        public string Municipality { get; set; }

        [JsonProperty(PropertyName = "municipality_id")]
        public string MunicipalityID { get; set; }

        [JsonProperty(PropertyName = "primary_stop_name")]
        public string PrimaryStopName { get; set; }

        [JsonProperty(PropertyName = "road_type_primary")]
        public string PrimaryRoadType { get; set; }

        [JsonProperty(PropertyName = "second_stop_name")]
        public string SecondaryStopName { get; set; }

        [JsonProperty(PropertyName = "road_type_second")]
        public string SecondaryRoadType { get; set; }

        [JsonProperty(PropertyName = "gps")]
        public Gps GPSInfo { get; set; }
    }
}
