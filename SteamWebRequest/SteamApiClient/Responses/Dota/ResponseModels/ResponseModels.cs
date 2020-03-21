using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaCosmeticRaritiesResult
    {
        [JsonProperty("result")]
        public DotaCosmeticRaritiesContent Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaCosmeticRaritiesContent
    {
        public IReadOnlyList<DotaCosmeticRarity> Rarities { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaTeamInfosResponse
    {
        public DotaTeamInfosContent Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaTeamInfosContent
    {
        public uint Status { get; set; }
        public IReadOnlyList<DotaTeamInfo> Teams { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class EquipedCosmeticItems
    {
        public IReadOnlyList<EquipedItem> Items { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class HeroesResponseParent
    {
        [JsonProperty("result")]
        public HeroesResponse Content { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class ItemCreators
    {
        public IReadOnlyList<uint> Contributors { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class LeagueListingResponse
    {
        public LeagueListing Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class LeagueListing
    {
        public IReadOnlyList<League> Leagues { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class LiveLeagueMatchResponse
    {
        public LiveLeagueMatchContens Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class LiveLeagueMatchContens
    {
        public uint Status { get; set; }
        public IReadOnlyList<LiveLeagueMatch> Games { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class MatchHistoryBySeqResponseParent
    {
        public MatchHistoryBySeqResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class MatchHistoryResponseParent
    {
        public MatchHistoryResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class TournamentInfoCollection
    {
        public IReadOnlyList<TournamentInfo> Infos { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class TournamentPlayerStatsResponse
    {
        public TournamentPlayerStats Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class ItemsResponseParent
    {
        [JsonProperty("result")]
        public ItemsResponse Result { get; set; }
    }
}
