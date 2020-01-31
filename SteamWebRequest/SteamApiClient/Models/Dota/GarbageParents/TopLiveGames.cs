using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    internal class TopLiveGames
    {
        [JsonProperty("game_list")]
        public IReadOnlyCollection<LiveMatch> Games { get; set; }
    }
}
