using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Departure : Item
    {
        [JsonProperty(PropertyName = "platform")]
        public Platform Platform { get; set; }

        [JsonProperty(PropertyName = "run")]
        public Run Run { get; set; }

        [JsonProperty(PropertyName = "time_timetable_utc")]
        public DateTime? ScheduledTime { get; set; }

        [JsonProperty(PropertyName = "time_realtime_utc")]
        public DateTime? EstimatedTime { get; set; }

        [JsonProperty(PropertyName = "flags")]
        [JsonConverter(typeof(DepartureFlagsConverter))]
        public DepartureFlags Flags { get; set; }

        [JsonProperty(PropertyName = "disruptions")]
        public DisruptionArray Disruptions { get; set; }
    }
}
