using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class StopFacilities : Item
    {
        [JsonProperty(PropertyName = "stop_id")]
        public string StopID { get; set; }

        [JsonProperty(PropertyName = "stop_mode_id")]
        public string StopModeID { get; set; }

        [JsonProperty(PropertyName = "stop_type")]
        public string StopType { get; set; }

        [JsonProperty(PropertyName = "stop_type_description")]
        public string StopTypeDescryption { get; set; }

        [JsonProperty(PropertyName = "location")]
        public StopFacilitiesLocation StopLocation { get; set; }

        [JsonProperty(PropertyName = "amenity")]
        public Amenity Amenity { get; set; }

        [JsonProperty(PropertyName = "accessibility")]
        public Accessibility Accessibility { get; set; }
    }
}
