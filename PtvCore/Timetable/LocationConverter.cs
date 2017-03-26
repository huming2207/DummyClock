using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    public class LocationConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Location))
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
            var location = JObject.Load(reader);

            if (location.Property("outlet_type ") != null && location.Property("business_name") != null)
            {
                var outlet = location.ToObject<Outlet>();
                return outlet;
            }
            else if (location.Property("transport_type") != null && location.Property("stop_id") != null)
            {
                var stop = location.ToObject<Stop>();
                return stop;
            }
            else
            {
                throw new TimetableException()
                {
                    Json = location.ToString()
                };
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
