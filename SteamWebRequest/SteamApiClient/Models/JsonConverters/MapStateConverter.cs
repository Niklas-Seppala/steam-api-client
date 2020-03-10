using Newtonsoft.Json;
using System;
using SteamApi.Utility;

namespace SteamApi
{
    public class MapStateConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => false;
        private readonly Type _type = typeof(int);

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(BarracksStatus))
                return ExpandValues.GetBarrackStatus(Convert.ToInt32(reader.Value));
            else if (objectType == typeof(TowerStatus))
                return ExpandValues.GetTowerStatus(Convert.ToInt32(reader.Value));
            else
                return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Cant write");
        }

        public override bool CanConvert(Type objectType)
        {
            return _type == objectType;
        }
    }
}
