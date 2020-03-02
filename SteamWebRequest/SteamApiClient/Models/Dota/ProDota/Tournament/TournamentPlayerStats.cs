using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class TournamentPlayerStats
    {
        public uint Status { get; set; }

        [JsonProperty("account_id")]
        public uint AccountId { get; set; }

        [JsonProperty("persona")]
        public string PersonaName { get; set; }

        public uint Wins { get; set; }
        public uint Losses { get; set; }
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
        public uint HeroDamageAverage { get; set; }

        [JsonProperty("tower_damage_average")]
        public uint TowerDamageAverage { get; set; }

        [JsonProperty("best_kills")]
        public uint BestKills { get; set; }

        [JsonProperty("best_kills_heroid")]
        public uint BestKillsHeroId { get; set; }

        [JsonProperty("best_gpm")]
        public uint BestGpm { get; set; }

        [JsonProperty("best_gpm_heroid")]
        public uint BestGpmHeroId { get; set; }

        [JsonProperty("best_networth")]
        public uint BestNetWorth { get; set; }

        [JsonProperty("best_networth_heroid")]
        public uint BestNetWorthHeroId { get; set; }

        [JsonProperty("heroes_played")]
        public IReadOnlyCollection<IReadOnlyDictionary<string, ushort>> HeroesPlayed { get; set; }

        public IReadOnlyCollection<TournamentPlayerStatsMatch> Matches { get; set; }
    }
}
