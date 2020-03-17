using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    [Serializable]
    public sealed class MatchDetailsResponse : ApiResponse
    {
        [JsonProperty("result")]
        public MatchDetails Contents { get; set; }
    }
}
