using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Steam
{
    /// <summary>
    /// Steam Profile model.
    /// </summary>
    [Serializable]
    public sealed class SteamAccount
    {
        /// <summary>
        /// Unix epoch timestamp of creation datetime
        /// </summary>
        public ulong TimeCreated { get; set; }

        /// <summary>
        /// Unix epoch timestamp for last logoff.
        /// Notes: usually private information
        /// </summary>
        public ulong LastLogOff { get; set; }

        /// <summary>
        /// 64-bit Steam id
        /// </summary>
        [JsonProperty("steamid")]
        public ulong Id64 { get; set; }

        /// <summary>
        /// Small (32x32) avatar image URL
        /// </summary>
        [JsonProperty("avatar")]
        public string AvatarSmallURL { get; set; }

        /// <summary>
        /// Medium (64x64) avatar image URL
        /// </summary>
        [JsonProperty("avatarmedium")]
        public string AvatarMediumURL { get; set; }

        /// <summary>
        /// Full (184x184) avatar image URL
        /// </summary>
        [JsonProperty("avatarfull")]
        public string AvatarFullURL { get; set; }

        /// <summary>
        /// Steam profile URL
        /// </summary>
        public string ProfileURL { get; set; }

        /// <summary>
        ///     1 = Private,
        ///     2 = Friends Only,
        ///     3 = Friends of firends,
        ///     4 = Users Only,
        ///     5 = Public
        /// </summary>
        public uint CommunityVisibilityState { get; set; }

        /// <summary>
        /// If set to 1 the user has configured the profile.
        /// </summary>
        public uint ProfileState { get; set; }

        /// <summary>
        /// Steam username.
        /// </summary>
        public string PersonaName { get; set; }

        // <summary>
        /// Users current profile state:
        ///     0 = Offline OR private profile,
        ///     1 = Online,
        ///     2 = Busy,
        ///     3 = Away,
        ///     4 = Snooze,
        ///     5 = Looking to trade,
        ///     6 = Looking to play
        /// </summary>
        public uint PersonaState { get; set; }

        /// <summary>
        /// ISO 3166 country code.
        /// </summary>
        public string LocCountryCode { get; set; }

        /// <summary>
        /// Steam state code
        /// </summary>
        public string LocStateCode { get; set; }

        /// <summary>
        /// Steam city code
        /// </summary>
        public uint LocCityId { get; set; }

        /// <summary>
        /// Player's clan id
        /// </summary>
        [JsonProperty("primaryclanid")]
        public string PrimaryClanId { get; set; }

        /// <summary>
        /// Current game
        /// </summary>
        [JsonProperty("gameextrainfo")]
        public string Game { get; set; }
    }
}
