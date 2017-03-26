using Newtonsoft.Json;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Accessibility : Item
    {
        [JsonProperty(PropertyName = "lighting")]
        public bool HasLighting { get; set; }

        [JsonProperty(PropertyName = "stairs")]
        public bool HasStairs { get; set; }

        [JsonProperty(PropertyName = "escalator")]
        public bool HasEscalator { get; set; }

        [JsonProperty(PropertyName = "lifts")]
        public bool HasLifts { get; set; }

        [JsonProperty(PropertyName = "hearing_loop")]
        public bool HasHearingLoop { get; set; }

        [JsonProperty(PropertyName = "tactile_tiles")]
        public bool HasTactileTiles { get; set; }

        [JsonProperty(PropertyName = "wheelchair")]
        public Wheelchair Wheelchair { get; set; }
    }
}
