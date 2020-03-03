using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class TopLiveGames
    {
        [JsonProperty("game_list")]
        public IReadOnlyList<LiveMatch> Games { get; set; }
    }
}
