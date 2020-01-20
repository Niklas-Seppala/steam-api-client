using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public sealed class MatchHistory
    {
        [JsonProperty("Result")]
        public MatchHistoryContent Content { get; set; }
    }
}
