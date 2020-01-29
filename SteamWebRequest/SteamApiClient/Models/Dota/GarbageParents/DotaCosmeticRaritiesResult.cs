using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    internal class DotaCosmeticRaritiesResult
    {
        [JsonProperty("result")]
        public DotaCosmeticRaritiesContent Result { get; set; }
    }
}
