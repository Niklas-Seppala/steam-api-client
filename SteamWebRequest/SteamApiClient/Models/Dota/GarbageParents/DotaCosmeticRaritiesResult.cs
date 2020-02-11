using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    internal class DotaCosmeticRaritiesResult
    {
        [JsonProperty("result")]
        public DotaCosmeticRaritiesContent Result { get; set; }
    }
}
