using Newtonsoft.Json;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Amenity : Item
    {
        [JsonProperty(PropertyName = "toilet")]
        public bool HasToilet { get; set; }

        [JsonProperty(PropertyName = "taxi_rank")]
        public bool HasTaxiRank { get; set; }

        [JsonProperty(PropertyName = "car_parking")]
        public int CarParkAmount { get; set; }

        [JsonProperty(PropertyName = "cctv")]
        public bool HasCctv { get; set; }
    }
}
