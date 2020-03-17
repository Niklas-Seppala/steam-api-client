using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Steam
{
    /// <summary>
    /// App news model
    /// </summary>
    [Serializable]
    public sealed class AppNews
    {
        /// <summary>
        /// Id of the news item
        /// </summary>
        [JsonProperty("gid")]
        public string Id { get; set; }

        /// <summary>
        /// Is news item URL other other than steam
        /// </summary>
        [JsonProperty("is_external_url")]
        public bool IsExternalUrl { get; set; }

        /// <summary>
        /// Unixtimestamp of news release date
        /// </summary>
        public ulong Date { get; set; }

        [JsonProperty("feed_type")]
        public uint FeedType { get; set; }

        /// <summary>
        /// News item title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL to news item
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// News item author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// News item contents
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// News item feed label
        /// </summary>
        public string FeedLabel { get; set; }

        /// <summary>
        /// App id that news item is about
        /// </summary>
        public uint AppId { get; set; }

        /// <summary>
        /// News item feed name
        /// </summary>
        public string FeedName { get; set; }
    }
}
