using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    internal sealed class MatchDetailsContainer
    {
        [JsonProperty("result")]
        public MatchDetails Details { get; set; }
    }
}
