﻿using System.Collections.Specialized;
using System;
using Newtonsoft.Json;
using SteamWebRequest.Models;

namespace SteamWebRequest
{
    public class MapStateConverter : JsonConverter
    {
        public override bool CanRead { get => true; }
        public override bool CanWrite { get => false; }
        private readonly Type _type = typeof(int);

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (objectType == typeof(BarracksStatus))
                return new BarracksStatus(new BitVector32(Convert.ToByte(reader.Value)));
            else if (objectType == typeof(TowerStatus))
                return new TowerStatus(new BitVector32(Convert.ToUInt16(reader.Value)));
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
