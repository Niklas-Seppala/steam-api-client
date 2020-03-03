using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class DcpResults
    {
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }
        public uint Standing { get; set; }
        public uint Points { get; set; }
        public double Earnings { get; set; }
        public ulong Timestamp { get; set; }
    }
}
