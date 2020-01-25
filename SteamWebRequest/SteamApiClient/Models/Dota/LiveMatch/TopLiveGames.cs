using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class TopLiveGames
    {
        [JsonProperty("game_list")]
        public List<LiveMatch> Games { get; set; }
    }
}
