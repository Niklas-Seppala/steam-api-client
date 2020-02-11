using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    internal class GameItems
    {
        [JsonProperty("result")]
        public ItemsContent Content { get; set; }
    }
}
