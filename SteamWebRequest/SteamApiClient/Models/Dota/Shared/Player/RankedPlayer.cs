using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class RankedPlayer
    {
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        public string Sponsor { get; set; }

        public string Country { get; set; }

        public ushort Rank { get; set; }

        public string Name { get; set; }
    }
}
