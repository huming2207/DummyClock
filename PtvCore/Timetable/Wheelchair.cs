using Newtonsoft.Json;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Wheelchair
    {
        [JsonProperty(PropertyName = "accessible_ramp")]
        public bool HasAccessableRamp { get; set; }

        [JsonProperty(PropertyName = "accessible_parking")]
        public bool HasAccessibleParking { get; set; }

        [JsonProperty(PropertyName = "accessible_phone")]
        public bool HasAccessiblePhone { get; set; }

        [JsonProperty(PropertyName = "accessible_toilet")]
        public bool HasAccessibleToilet { get; set; }
    }
}
