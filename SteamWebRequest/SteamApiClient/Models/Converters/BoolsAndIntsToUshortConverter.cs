using Newtonsoft.Json;
using System;
using System.Linq;

namespace SteamApi
{
    public class BoolsAndIntsToUshortConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type[] _types = { typeof(ushort), typeof(bool) };

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            return this.CanConvert(objectType) ? Convert.ToUInt16(existingValue) : (ushort)0;
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
