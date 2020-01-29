using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class TournamentPlayerStats
    {
        public byte Status { get; set; }

        [JsonProperty("account_id")]
        public uint AccountId { get; set; }

        [JsonProperty("persona")]
        public string PersonaName { get; set; }

        public byte Wins { get; set; }
        public byte Losses { get; set; }
        public ushort Kills { get; set; }
        public ushort Deaths { get; set; }
        public ushort Assists { get; set; }

        [JsonProperty("kills_avarage")]
        public double KillAverage { get; set; }

        [JsonProperty("deaths_average")]
        public double DeathAverage { get; set; }

        [JsonProperty("assists_average")]
        public double AssistAverage { get; set; }

        [JsonProperty("gpm_average")]
        public double GpmAverage { get; set; }

        [JsonProperty("xpm_average")]
        public double XpmAverage { get; set; }

        [JsonProperty("networth_average")]
        public double NetWorthAverage { get; set; }

        [JsonProperty("last_hits_average")]
        public double LastHitAverage { get; set; }

        [JsonProperty("hero_damage_average")]
        public ushort HeroDamageAverage { get; set; }

        [JsonProperty("tower_damage_average")]
        public ushort TowerDamageAverage { get; set; }

        [JsonProperty("best_kills")]
        public byte BestKills { get; set; }

        [JsonProperty("best_kills_heroid")]
        public byte BestKillsHeroId { get; set; }

        [JsonProperty("best_gpm")]
        public ushort BestGpm { get; set; }

        [JsonProperty("best_gpm_heroid")]
        public ushort BestGpmHeroId { get; set; }

        [JsonProperty("best_networth")]
        public ushort BestNetWorth { get; set; }

        [JsonProperty("best_networth_heroid")]
        public ushort BestNetWorthHeroId { get; set; }

        [JsonProperty("heroes_played")]
        public IReadOnlyCollection<IReadOnlyDictionary<string, ushort>> HeroesPlayed { get; set; }

        public IReadOnlyCollection<TournamentPlayerStatsMatch> Matches { get; set; }
    }
}
