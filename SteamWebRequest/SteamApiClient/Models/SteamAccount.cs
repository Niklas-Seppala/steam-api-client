using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class AccountCollection
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
        public List<SteamAccount> Accounts => this.Content.Accounts;
    }

    public sealed class AccountCollectionContent
    {
        [JsonProperty("players")]
        public List<SteamAccount> Accounts { get; set; }
    }

    public sealed class SteamAccount
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeCreated { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastLogOff { get; set; }

        [JsonProperty("steamid")]
        public string Id { get; set; }

        [JsonProperty("avatar")]
        public string AvatarURL { get; set; }

        [JsonProperty("avatarmedium")]
        public string AvatarMediumURL { get; set; }

        [JsonProperty("avatarfull")]
        public string AvatarFullURL { get; set; }

        public string ProfileURL { get; set; }
        public byte CommunityVisibilityState { get; set; }
        public byte ProfileState { get; set; }
        public string PersonaName { get; set; }
        public byte PersonaState { get; set; }
        public string LocCountryCode { get; set; }
        public string LocStateCode { get; set; }
        public int LocCityId { get; set; }
    }
}
