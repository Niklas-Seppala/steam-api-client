using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaSchemaUrlResponseParent
    {
        public SchemaUrlResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaItemIconPathResponseParent
    {
        public ItemIconPathResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaCosmeticRaritiesResponseParent
    {
        [JsonProperty("result")]
        public CosmeticRaritiesResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class DotaTeamInfosResponseParent
    {
        public DotaTeamInfosResponse Result { get; set; }
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
    internal sealed class LeagueListingResponseParent
    {
        public LeagueListingResponse Result { get; set; }
    }

    /// <summary>
    /// This is a JSON response container class that is never supposed
    /// to see sunlight during its brief lifetime.
    /// </summary>
    internal sealed class LiveLeagueMatchesResponseParent
    {
        public LiveLeagueMatchesResponse Result { get; set; }
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
    internal sealed class ItemsResponseParent
    {
        [JsonProperty("result")]
        public ItemsResponse Result { get; set; }
    }
}
