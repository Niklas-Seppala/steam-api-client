using Newtonsoft.Json;
using System;
using System.Linq;

namespace SteamApi
{
    public class TimeSpanConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type[] types = new[] { typeof(int), typeof(double) };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Can't write");
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            return TimeSpan.FromSeconds(Convert.ToDouble(reader.Value));
        }

        public override bool CanConvert(Type objectType)
        {
            return types.Any(t => t == objectType);
        }
    }

}
