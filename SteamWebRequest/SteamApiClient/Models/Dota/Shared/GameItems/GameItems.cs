using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    internal class GameItems
    {
        [JsonProperty("result")]
        public ItemsContent Content { get; set; }
    }
}
