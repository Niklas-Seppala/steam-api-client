using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    public class AppNews
    {
        [JsonProperty("gid")]
        public string Id { get; set; }
        [JsonProperty("is_external_url")]
        public bool IsExternalUrl { get; set; }
        public ulong Date { get; set; }
        [JsonProperty("feed_type")]
        public uint FeedType { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Contents { get; set; }
        public string FeedLabel { get; set; }
        public uint AppId { get; set; }
        public string FeedName { get; set; }
    }
}
