using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    internal sealed class MatchHistoryContainer
    {
        [JsonProperty("result")]
        public MatchHistoryResponse History { get; set; }
    }
}
