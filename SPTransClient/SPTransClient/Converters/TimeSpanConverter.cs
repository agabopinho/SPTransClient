using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPTransClient.Converters
{
    public class TimeSpanConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is TimeSpan) && (!(value is Nullable) || Nullable.GetUnderlyingType(value.GetType()) != typeof(TimeSpan)))
            {
                throw new JsonSerializationException("Property type not is TimeSpan");
            }

            if (value == null)
            {
                writer.WriteNull();
            }

            var time = (TimeSpan)value;

            writer.WriteValue(time.ToString("HH:mm"));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (objectType != typeof(TimeSpan) && Nullable.GetUnderlyingType(objectType) != typeof(TimeSpan))
            {
                throw new JsonSerializationException("Property type not is TimeSpan");
            }

            if (reader.Value == null || reader.Value.ToString().Trim() == string.Empty)
            {   return null;
             
            }

            var parts = reader.Value.ToString().Split(':');

            return new TimeSpan(int.Parse(parts[0]), int.Parse(parts[1]), 0);
        }
    }
}
