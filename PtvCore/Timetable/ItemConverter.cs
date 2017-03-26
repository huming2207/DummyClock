using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    public class ItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Item))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var resultWrapper = JObject.Load(reader);
            var type = resultWrapper["type"].Value<string>();
            var result = resultWrapper["result"];

            switch (type)
            {
                case "stop":
                    var stop = result.ToObject<Stop>();
                    return stop;

                case "line":
                    var line = result.ToObject<Line>();
                    return line;

                default:
                    throw new TimetableException()
                    {
                        Json = resultWrapper.ToString()
                    };
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
