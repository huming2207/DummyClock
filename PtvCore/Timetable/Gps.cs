using Newtonsoft.Json;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Gps : Item
    {
        [JsonProperty(PropertyName = "longitude", NullValueHandling = NullValueHandling.Include)]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude", NullValueHandling = NullValueHandling.Include)]
        public double Latitude { get; set; }
    }
}
