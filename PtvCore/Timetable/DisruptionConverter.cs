using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace PtvCore.Timetable
{
    public class DisruptionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Disruption[]))
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
            var disruptionWrapper = JObject.Load(reader);

            if (disruptionWrapper.Property("metro-train") != null)
            {
                return SeperateResult("metro-train", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("metro-bus") != null)
            {
                return SeperateResult("metro-bus", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("metro-tram") != null)
            {
                return SeperateResult("metro-tram", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("regional-train") != null)
            {
                return SeperateResult("regional-train", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("regional-bus") != null)
            {
                return SeperateResult("metro-bus", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("regional-coach") != null)
            {
                return SeperateResult("metro-coach", disruptionWrapper);
            }
            else if (disruptionWrapper.Property("general") != null)
            {
                return SeperateResult("general", disruptionWrapper);
            }
            else
            {
                throw new TimetableException()
                {
                    Json = disruptionWrapper.ToString()
                };
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private object SeperateResult(string jsonPropertyName, JObject disruptionWrapper)
        {
            var disruptionMessage = disruptionWrapper[jsonPropertyName];
            Disruption[] result = disruptionMessage.ToObject<Disruption[]>();
            return result;
        }

    }
}
