using Newtonsoft.Json;
using System;
using System.Linq;

namespace SteamApi
{
    public class BoolToIntConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type[] _types = { typeof(uint), typeof(bool) };

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            return CanConvert(objectType) ? Convert.ToUInt32(existingValue) : 0;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any((type) => type == objectType);
        }
    }
}
