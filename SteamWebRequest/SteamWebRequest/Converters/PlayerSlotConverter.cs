using System.Collections.Specialized;
using System;
using Newtonsoft.Json;

namespace SteamWebRequest
{
    public class PlayerSlotConverter : JsonConverter
    {
        public override bool CanRead { get => true; }
        public override bool CanWrite { get => false; }
        private readonly Type _type = typeof(int);

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            return new BitVector32(Convert.ToInt32(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Cant write");
        }

        public override bool CanConvert(Type objectType)
        {
            return _type == typeof(int);
        }
    }
}
