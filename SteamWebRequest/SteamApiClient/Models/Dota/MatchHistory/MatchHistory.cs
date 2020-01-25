using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    public sealed class MatchHistory
    {
        [JsonProperty("result")]
        public MatchHistoryContent History { get; set; }
    }
}
