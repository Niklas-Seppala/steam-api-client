using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    [Serializable]
    public sealed class TopLiveGamesResponse : ApiResponse
    {
        [JsonProperty("game_list")]
        public IReadOnlyList<LiveMatch> Contents { get; set; }
    }
}
