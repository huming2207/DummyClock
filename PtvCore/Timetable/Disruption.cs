using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PtvCore.Timetable
{
    [JsonObject()]
    public class Disruption : Item
    {
        [JsonProperty(PropertyName = "disruption_id")]
        public string DisruptionId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string MessageURL { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName ="status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "publishedOn")]
        public DateTime? PublishTime { get; set; }

        [JsonProperty(PropertyName = "lines")]
        public LineArray Lines { get; set; }

        [JsonProperty(PropertyName = "direction")]
        public Direction Direction { get; set; }

        [JsonProperty(PropertyName ="fromDate")]
        public DateTime? StartTime { get; set; }

        [JsonProperty(PropertyName = "toDate")]
        public DateTime? EndTime { get; set; }
    }    
}
