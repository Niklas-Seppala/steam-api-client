using Newtonsoft.Json;
using System;
using SteamApi.Responses.Dota;

namespace SteamApi
{
    public class PlayerSlotConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type _type = typeof(uint);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new PlayerSlot(Convert.ToInt32(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Can't write");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _type;
        }
    }
}
