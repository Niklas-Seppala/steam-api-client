using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    internal sealed class MatchHistoryContainer
    {
        [JsonProperty("result")]
        public MatchHistoryResponse History { get; set; }
    }
}
