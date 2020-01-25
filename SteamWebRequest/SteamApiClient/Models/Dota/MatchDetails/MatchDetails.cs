using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    public sealed class MatchDetails
    {
        [JsonProperty("result")]
        public MatchDetailsContent Details { get; set; }
    }
}
