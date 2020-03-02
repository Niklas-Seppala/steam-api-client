using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApi.Models
{

    public sealed class SteamAccount
    {
        //[JsonConverter(typeof(UnixDateTimeConverter))]
        //public DateTime? TimeCreated { get; set; }
        public ulong TimeCreated { get; set; }

        //[JsonConverter(typeof(UnixDateTimeConverter))]
        //public DateTime? LastLogOff { get; set; }

        public ulong LastLogOff { get; set; }

        [JsonProperty("steamid")]
        public string Id { get; set; }

        [JsonProperty("avatar")]
        public string AvatarURL { get; set; }

        [JsonProperty("avatarmedium")]
        public string AvatarMediumURL { get; set; }

        [JsonProperty("avatarfull")]
        public string AvatarFullURL { get; set; }

        public string ProfileURL { get; set; }
        public uint CommunityVisibilityState { get; set; }
        public uint ProfileState { get; set; }
        public string PersonaName { get; set; }
        public uint PersonaState { get; set; }
        public string LocCountryCode { get; set; }
        public string LocStateCode { get; set; }
        public uint LocCityId { get; set; }
    }
}
