using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using SteamApi.Utility;

namespace SteamApi
{
    public class PlayerSlotConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type _type = typeof(int);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ExpandValues.GetPlayerSlot(Convert.ToInt32(reader.Value));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Cant write");
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _type;
        }
    }
}
