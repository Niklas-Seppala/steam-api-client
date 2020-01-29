using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    internal sealed class MatchDetailsContainer
    {
        [JsonProperty("result")]
        public MatchDetails Details { get; set; }
    }
}
