using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtvCore.Timetable
{
    public class DepartureFlagsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DepartureFlags))
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
            var rawFlags = JValue.Load(reader).ToString();

            if (string.IsNullOrEmpty(rawFlags))
            {
                return DepartureFlags.None;
            }

            DepartureFlags flags = DepartureFlags.HasFlags;

            if (rawFlags.Contains("RR"))
            {
                flags |= DepartureFlags.ReservationsRequired;
            }

            if (rawFlags.Contains("GC"))
            {
                flags |= DepartureFlags.GuaranteedConnection;
            }

            if (rawFlags.Contains("DOO"))
            {
                flags |= DepartureFlags.DropOffOnly;
            }

            if (rawFlags.Contains("PUO"))
            {
                flags |= DepartureFlags.PickUpOnly;
            }

            if (rawFlags.Contains("MO"))
            {
                flags |= DepartureFlags.MondaysOnly;
            }

            if (rawFlags.Contains("TU"))
            {
                flags |= DepartureFlags.TuesdaysOnly;
            }

            if (rawFlags.Contains("WE"))
            {
                flags |= DepartureFlags.WednesdaysOnly;
            }

            if (rawFlags.Contains("TH"))
            {
                flags |= DepartureFlags.ThursdaysOnly;
            }

            if (rawFlags.Contains("FR"))
            {
                flags |= DepartureFlags.FridaysOnly;
            }

            if (rawFlags.Contains("SS"))
            {
                flags |= DepartureFlags.SchoolDaysOnly;
            }

            return flags;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
