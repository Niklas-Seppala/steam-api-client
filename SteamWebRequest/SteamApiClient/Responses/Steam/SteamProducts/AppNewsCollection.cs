using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    /// <summary>
    /// Collection of news about app
    /// </summary>
    [Serializable]
    public sealed class AppNewsCollection
    {
        /// <summary>
        /// Id of the app that the news item is about
        /// </summary>
        public uint AppId { get; set; }

        /// <summary>
        /// Total count of news items about the app
        /// </summary>
        [JsonProperty("count")]
        public uint TotalCount { get; set; }

        /// <summary>
        /// List of the news items about the app
        /// </summary>
        public IReadOnlyList<AppNews> NewsItems { get; set; }
    }
}
