using Newtonsoft.Json.Linq;
using SteamApi.Models.Dota;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApi
{
    public class DotaApiClient : ApiClient
    {
        protected override string TestUrl => $"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1?key={ApiKey}";
        #region [Url Constants]
        private const string URL_ITEM_IMG = "http://media.steampowered.com/apps/dota2/images/items/";
        private const string URL_HERO_IMG = "http://media.steampowered.com/apps/dota2/images/heroes/";
        private const string URL_HEROINFOS = "http://www.dota2.com/jsfeed/heropickerdata";
        private const string URL_ITEMINFOS = "http://www.dota2.com/jsfeed/itemdata";
        private const string URL_INT_PRIZEPOOL = "http://www.dota2.com/jsfeed/intlprizepool";
        private const string URL_HEROPEDIA_DATA = "http://www.dota2.com/jsfeed/heropediadata/";
        private const string URL_ABILITY_DATA = "http://www.dota2.com/jsfeed/abilitydata";
        private const string URL_UNIQUE_USERS = "http://www.dota2.com/jsfeed/uniqueusers";
        private const string URL_LEADERBOARDS = "http://www.dota2.com/webapi/ILeaderboard/GetDivisionLeaderboard/v1/";
        private const string URL_IDOTA2DPC = "https://www.dota2.com/webapi/IDOTA2DPC/";
        private const string HOST = "api.steampowered.com";
        private const string IDOTA2_FANTASY = "IDOTA2Fantasy_205790";
        private const string IDOTA2_MATCH_STATS = "IDOTA2MatchStats_570";
        private const string IDOTA2_MATCH = "IDOTA2Match_570";
        private const string IDOTA2_TICKET = "IDOTA2Ticket_570";
        private const string IECONDOTA_BETA = "IEconDOTA2_205790";
        private const string IECONDOTA = "IEconDOTA2_570";
        private const string IECON_ITEMS = "IEconItems_570";
        #endregion

        public DotaApiClient(bool testConnection = false, string schema = "https")
            : base(testConnection, schema) { }

        #region [Matches]
        /// <summary>
        /// Sends GET request to api.steampowered.com for match history
        /// by sequence number. Default interface is IDOTA2Match_570.
        /// Returns list of ! MatchDetails ! unlike other match history methods.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IList<MatchDetails>> GetMatchHistoryBySequenceNumAsync(
            ulong seqNum, string apiInterface = IDOTA2_MATCH, uint count = 50, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetMatchHistoryBySequenceNum", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("start_at_match_seq_num", seqNum.ToString())
                .AddQuery("matches_requested", count.ToString());
            return (await GetModelAsync<MatchHistoryBySeqResponse>(cToken: cToken).ConfigureAwait(false)).Result.Matches;
        }

        /// <summary>
        /// Gets match history model. Request can be specified by providing optional parameters
        /// and can be cancelled by providing CancellationToken.
        /// </summary>
        /// <param name="minPlayers">minimum number of human players</param>
        /// <param name="skillLevel"> the average skill range of the match, these can be 1-3.
        /// Ignored if an account id is specified</param>
        public async Task<MatchHistoryResponse> GetMatchHistoryAsync(uint playerId32 = 0, uint heroId = 0,
            uint minPlayers = 0, uint leagueId = 0, ulong startAtId = 0, uint count = 25,
            string apiInterface = IDOTA2_MATCH, uint skillLevel = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetMatchHistory", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("account_id", playerId32.ToString())
                .AddQuery("matches_requested", count.ToString())
                .AddQuery("hero_id", heroId.ToString())
                .AddQuery("min_players", minPlayers.ToString())
                .AddQuery("league_id", leagueId.ToString())
                .AddQuery("start_at_match_id", startAtId.ToString())
                .AddQuery("skill", skillLevel.ToString());
            return (await GetModelAsync<MatchHistoryContainer>(cToken: cToken).ConfigureAwait(false)).History;
        }

        /// <summary>
        /// Sends GET request for Dota 2 match details. Request can be
        /// cancelled by providing cancellation token.
        /// </summary>
        public async Task<MatchDetails> GetMatchDetailsAsync(string matchId,
            string apiInterface = IDOTA2_MATCH, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(apiInterface, "GetMatchDetails", "v1")
               .AddQuery("key", ApiKey)
               .AddQuery("match_id", matchId.ToString())
               .AddQuery("include_persona_names", "1");
            return (await GetModelAsync<MatchDetailsContainer>(cToken: cToken).ConfigureAwait(false)).Details;
        }

        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="partner">partner id</param>
        public async Task<IReadOnlyCollection<LiveMatch>> GetTopLiveGamesAsync(string apiInterface = IDOTA2_MATCH,
            int partner = 1, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetTopLiveGame", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString());
            return (await GetModelAsync<TopLiveGames>(cToken: cToken).ConfigureAwait(false)).Games;
        }
        #endregion
        #region [Game Constant Data]
        /// <summary>
        /// Sends GET request for dota heroinfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, HeroInfo>> GetHeroInfosAsync(CToken cToken = default)
        {
            return await GetModelAsync<IReadOnlyDictionary<string, HeroInfo>>(URL_HEROINFOS, cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 hero stats. Requst
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, HeroStats>> GetHeroStatsAsync(CToken cToken = default)
        {
            UrlBuilder.UrlFromString(URL_HEROPEDIA_DATA)
                .AddQuery("feeds", "herodata");
            return (await GetModelAsync<HeroStatsContainer>(cToken: cToken).ConfigureAwait(false)).HeroStats;
        }

        /// <summary>
        /// Sends GET request for dota 2 heroes. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<Hero>> GetHeroesAsync(string lang = "en",
            string apiInterface = IECONDOTA, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetHeroes", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);
            return (await GetModelAsync<HeroesResponse>(cToken: cToken).ConfigureAwait(false)).Content.Heroes;
        }

        /// <summary>
        /// Sends GET request to dota2.com for Dota 2 iteminfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Item>> GetItemInfoDictAsync(CToken cToken = default)
        {
            return (await GetModelAsync<ItemDictionary>(URL_ITEMINFOS, cToken).ConfigureAwait(false)).ItemDict;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for dota 2 items. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<Item>> GetGameItemsAsync(string lang = "en",
            string apiInterface = IECONDOTA, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetGameItems", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);
            return (await GetModelAsync<GameItems>(cToken: cToken).ConfigureAwait(false)).Content.Items;
        }

        /// <summary>
        /// Sends GET request for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Ability>> GetAbilitiesDictAsync(CToken cToken = default)
        {
            return (await GetModelAsync<Abilities>(URL_ABILITY_DATA, cToken).ConfigureAwait(false)).AbilityDict;
        }
        #endregion
        #region [Users]
        /// <summary>
        /// Sends GET request for unique user count. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, uint>> GetUniqueUsersAsync(CToken cToken = default)
        {
            return await GetModelAsync<Dictionary<string, uint>>(URL_UNIQUE_USERS, cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 leaderboards. Specify
        /// region. Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<Leaderboard> GetLeaderboardAsync(DotaRegion region = default, CToken cToken = default)
        {
            UrlBuilder.UrlFromString(URL_LEADERBOARDS);
            switch (region)
            {
                case DotaRegion.Europe:
                    UrlBuilder.AddQuery("division", "europe");
                    break;
                case DotaRegion.America:
                    UrlBuilder.AddQuery("division", "americas");
                    break;
                case DotaRegion.SEA:
                    UrlBuilder.AddQuery("division", "se_asia");
                    break;
                case DotaRegion.China:
                    UrlBuilder.AddQuery("division", "china");
                    break;
            }
            return await GetModelAsync<Leaderboard>(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 player profile.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<DotaPlayerProfile> GetDotaPlayerProfileAsync(string id32, CToken cToken = default)
        {
            UrlBuilder.UrlFromString(URL_IDOTA2DPC + "GetPlayerInfo/v001")
                .AddQuery("account_id", id32);

            return await GetModelAsync<DotaPlayerProfile>(cToken: cToken).ConfigureAwait(false);
        }
        #endregion
        #region [Cosmetic Items]
        /// <summary>
        /// Sends GET request to api.steampowered.com for players equiped
        /// items for specified hero. Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<EquipedItem>> GetEquipedPlayerItemsAsync(ulong steamId64,
            ushort heroClassId, string apiInterface = IECON_ITEMS, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetEquippedPlayerItems", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("steamid", steamId64.ToString())
                .AddQuery("class_id", heroClassId.ToString());
            var response = await GetModelAsync<IReadOnlyDictionary<string, EquipedCosmeticItems>>(cToken: cToken)
                .ConfigureAwait(false);
            return response["result"].Items;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for player's
        /// item inventory. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<PlayerInventory> GetPlayerItemsAsync(ulong steamid64,
            string apiInterface = IECON_ITEMS, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetPlayerItems", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("steamid", steamid64.ToString());
            var response = await GetModelAsync<IReadOnlyDictionary<string, PlayerInventory>>(cToken: cToken).ConfigureAwait(false);
            return response["result"];
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// item icon path.
        /// </summary>
        /// <param name="iconName">the item icon name to get the CDN path of</param>
        /// <param name="iconType">the type of image you want. 0 = normal, 1 = large, 2 = ingame</param>
        public async Task<string> GetItemIconPathAsync(string iconName, byte iconType = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IECONDOTA_BETA, "GetItemIconPath", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("iconname", iconName)
                .AddQuery("icontype", iconType.ToString());
            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken).ConfigureAwait(false));
            return response.result.path;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for item schema url.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiInterface">api interface</param>
        public async Task<string> GetSchemaUrlAsync(string apiInterface = IECON_ITEMS, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetSchemaURL", "v1")
                .AddQuery("key", ApiKey);
            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken).ConfigureAwait(false));
            return response.result.items_game_url;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for strore metadata.
        /// Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<StoreMetadata> GetStoreMetadataAsync(string apiInterface = IECON_ITEMS, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetStoreMetaData", "v1")
                .AddQuery("key", ApiKey);
            var response = await GetModelAsync<IReadOnlyDictionary<string, StoreMetadata>>(cToken: cToken).ConfigureAwait(false);
            return response["result"];
        }

        /// <summary>
        /// Sends GET request for item creator ids for
        /// specified item. Request can be cancelled
        /// by providing Cancellation token.
        /// </summary>
        /// <param name="itemDef">item id</param>
        public async Task<IReadOnlyCollection<uint>> GetItemCreatorsAsync(uint itemDef, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IECONDOTA, "GetItemCreators", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("itemdef", itemDef.ToString());
            var creators = await GetModelAsync<ItemCreators>(cToken: cToken).ConfigureAwait(false);
            return creators.Contributors;
        }

        /// <summary>
        /// Sends GET request for Dota 2 cosmetic item rarities.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<DotaCosmeticRarity>> GetCosmeticRaritiesAsync(CToken cToken = default,
            string apiInterface = IECONDOTA, string lang = "en")
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetRarities", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);
            var result = await GetModelAsync<DotaCosmeticRaritiesResult>(cToken: cToken).ConfigureAwait(false);
            return result.Result.Rarities;
        }
        #endregion
        #region [Get Tournament Data]
        /// <summary>
        /// Sends GET request to api.steampowered.com for players tournament
        /// stats. Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<TournamentPlayerStats> GetTournamentPlayerStatsAsync(uint accountId32, uint leagueId32,
            string apiInterface = IDOTA2_MATCH, string methodVersion = "v2", ushort heroId = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetTournamentPlayerStats", methodVersion)
                .AddQuery("key", ApiKey)
                .AddQuery("account_id", accountId32.ToString())
                .AddQuery("league_id", leagueId32.ToString())
                .AddQuery("hero_id", heroId.ToString());
            return (await GetModelAsync<TournamentPlayerStatsResponse>(cToken: cToken).ConfigureAwait(false)).Result;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for real time match stats.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<RealtimeMatchStats> GetRealtimeMatchStatsAsync(string serverId,
            CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_MATCH_STATS, "GetRealtimeStats", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("server_steam_id", serverId);
            return await GetModelAsync<RealtimeMatchStats>(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for live
        /// league games. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<LiveLeagueMatch>> GetLiveLeagueMatchAsync(ulong matchId64 = 0,
            uint leagueId32 = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_MATCH, "GetLiveLeagueGames", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("match_id", matchId64.ToString())
                .AddQuery("league_id", leagueId32.ToString());
            return (await GetModelAsync<LiveLeagueMatchResponse>(cToken: cToken).ConfigureAwait(false)).Result.Games;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for leaguelistings.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<League>> GetLeagueListingAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath("IDOTA2Match_205790", "GetLeagueListing", "v1")
                .AddQuery("key", ApiKey);
            return (await GetModelAsync<LeagueListingResponse>(cToken: cToken).ConfigureAwait(false)).Result.Leagues;
        }

        /// <summary>
        /// Sends GET request for dota 2 tournament prizepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, uint>> GetTournamentPrizePoolAsync(uint leagueId,
            string apiInterface = IECONDOTA, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetTournamentPrizePool", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("leagueid", leagueId.ToString());
            return (await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyDictionary<string, uint>>>(cToken: cToken)
                .ConfigureAwait(false))["result"];
        }

        /// <summary>
        /// Sends GET request for league node data.
        /// Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        public async Task<LeagueNode> GetLeagueNodeAsync(ulong leagueId, ulong nodeId, CToken cToken = default)
        {
            UrlBuilder.UrlFromString(URL_IDOTA2DPC + "GetLeagueNodeData/v001/")
                .AddQuery("league_id", leagueId.ToString())
                .AddQuery("node_id", nodeId.ToString());
            return await GetModelAsync<LeagueNode>(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for tournament infos beginning from provided
        /// Unix Timestamp. If Unix Timestamp is not provided request uses
        /// current timestamp. Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<TournamentInfo>> GetTournamentInfoAsync(long timestamp = -1,
            byte maxTier = 5, byte minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(ValidateTimestamp(timestamp), maxTier, minTier);
            return (await GetModelAsync<TournamentInfoCollection>(cToken: cToken).ConfigureAwait(false)).Infos;
        }

        /// <summary>
        /// Sends GET request for tournament infos beginnning
        /// from provided date.Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<TournamentInfo>> GetTournamentInfoAsync(DateTime datetime,
            byte maxTier = 5, byte minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(GetUnixTimestampFromDate(datetime), maxTier, minTier);
            return (await GetModelAsync<TournamentInfoCollection>(cToken: cToken).ConfigureAwait(false)).Infos;
        }

        /// <summary>
        /// Creates url for GetTournamentInfoAsync() methods
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <returns>UrlBuilder object that contains the url.</returns>
        private void CreateUrlForTournamentInfo(ulong timestamp, byte maxTier, byte minTier)
        {
            if (minTier > maxTier)
            {
                throw new APIException("Minimum tier can't be larger than maximum tier");
            }
            else
            {
                UrlBuilder.UrlFromString(URL_IDOTA2DPC + "GetLeagueInfoList/v1")
                    .AddQuery("start_timestamp", timestamp.ToString())
                    .AddQuery("min_tier", minTier.ToString())
                    .AddQuery("max_tier", maxTier.ToString());
            }
        }

        /// <summary>
        /// Sends GET request for Internation pricepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, uint>> GetInternationalPrizePoolAsync(CToken cToken = default)
        { 
            return await GetModelAsync<IReadOnlyDictionary<string, uint>>(URL_INT_PRIZEPOOL, cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for recent and upcoming DCP
        /// events. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<RecentDcpEvents> GetRecentDcpEventsAsync(CToken cToken = default)
        {
            return await GetModelAsync<RecentDcpEvents>(URL_IDOTA2DPC + "/GetRecentAndUpcomingMatches/v1", cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 team info. DCP results
        /// can be included to request. Request can be cancelled by provding
        /// cancellation token.
        /// </summary>
        public async Task<DotaTeam> GetDotaTeamAsync(string teamId,
            bool includeDCP = false, CToken cToken = default)
        {
            UrlBuilder.UrlFromString(URL_IDOTA2DPC + "/GetSingleTeamInfo/v001/")
                .AddQuery("team_id", teamId);
            if (includeDCP)
            {
                UrlBuilder.AddQuery("get_dpc_info", "1");
            }
            return await GetModelAsync<DotaTeam>(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for DotaTeamInfos by providing start team
        /// id. Default request size is 100 teams. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyCollection<DotaTeamInfo>> GetDotaTeamInfosByIdAsync(ulong startId = 1,
            uint count = 100, string apiInterface = IDOTA2_MATCH, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetTeamInfoByTeamID", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("start_at_team_id", startId.ToString())
                .AddQuery("teams_requested", count.ToString());
            return (await GetModelAsync<DotaTeamInfosResponse>(cToken: cToken).ConfigureAwait(false)).Result.Teams;
        }
        #endregion
        #region [Images]
        /// <summary>
        /// Sends GET request for hero image.
        /// </summary>
        public async Task<byte[]> GetHeroImageAsync(string heroName, ImageShape imgShape = ImageShape.Horizontal)
        {
            StringBuilder sBuilder = new StringBuilder(URL_HERO_IMG);
            sBuilder.Append(heroName);
            switch (imgShape)
            {
                case ImageShape.Vertical:
                    sBuilder.Append("_vert.jpg");
                    break;
                case ImageShape.Full:
                    sBuilder.Append("_full.png");
                    break;
                case ImageShape.Horizontal:
                    sBuilder.Append("_lg.png");
                    break;
                case ImageShape.Small:
                    sBuilder.Append("_sb.png");
                    break;
            }
            UrlBuilder.UrlFromString(sBuilder.ToString());
            return await GetBytesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for item image.
        /// </summary>
        public async Task<byte[]> GetItemImageAsync(string imgName, CToken cToken = default)
        {
            return await GetBytesAsync(URL_ITEM_IMG + imgName, cToken).ConfigureAwait(false);
        }
        #endregion
        #region [Unfinished] 
        /// <summary>
        /// Really don't know what this do :). In case
        /// you know, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetSteamAccountValidForBadgeType) Figure out what this method does!
        public async Task<string> GetSteamAccountValidForBadgeType(ulong steamid64, uint validBadgeType1,
            uint validBadgeType2, uint validBadgeType3, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_TICKET, "SteamAccountValidForBadgeType", "v1")
                .AddQuery("steamid", steamid64.ToString())
                .AddQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AddQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AddQuery("ValidBadgeType3", validBadgeType3.ToString());
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Really don't know what this do :). In case
        /// you know, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (ClaimBadgeReward) Figure out what this method does!
        public async Task<string> ClaimBadgeReward(string badgeId, uint validBadgeType1,
            uint validBadgeType2, uint validBadgeType3, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_TICKET, "ClaimBadgeReward", "v1")
                .AddQuery("BadgeID", badgeId)
                .AddQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AddQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AddQuery("ValidBadgeType3", validBadgeType3.ToString());
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Really don't know what this do :). In case
        /// you know, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetSteamIDForBadgeId) Figure out what this method does!
        public async Task<string> GetSteamIDForBadgeId(string badgeId, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_TICKET, "GetSteamIDForBadgeID", "v1")
                .AddQuery("BadgeID", badgeId);
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// event stats for specified account. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccount) Figure out response object model
        public async Task<string> GetEventStatsForAccount(uint eventId, uint accountId32,
            string lang = "en", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IECONDOTA_BETA, "GetEventStatsForAccount", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("eventid", eventId.ToString())
                .AddQuery("accountid", accountId32.ToString())
                .AddQuery("language", lang);
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// Fantasy pro player listing. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetProPlayerList) Figure out response object model
        public async Task<string> GetProPlayerList(CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_FANTASY, "GetProPlayerList", "v1")
                .AddQuery("key", ApiKey);
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered for fantasy
        /// player stats. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetFantasyPlayerStats) Figure out response object model
        public async Task<string> GetFantasyPlayerStats(uint fantasyLeagueId, string startTimestamp = "0",
            string endTimestamp = "0", ulong matchId = 0, uint seriesId = 0, uint playerId32 = 0,
            CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_FANTASY, "GetFantasyPlayerStats", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("StartTime", startTimestamp)
                .AddQuery("EndTime", endTimestamp)
                .AddQuery("MatchID", matchId.ToString())
                .AddQuery("SeriesID", seriesId.ToString())
                .AddQuery("PlayerAccountID", playerId32.ToString())
                .AddQuery("FantasyLeagueID", fantasyLeagueId.ToString());
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// pro players official fantasy info. Request can
        /// be cancelled by providing cancellation token.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetPlayerOfficialFantasyInfo) Figure out response object model
        public async Task<FantasyPlayerOfficialInfo> GetPlayerOfficialFantasyInfo(uint accountId32, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_FANTASY, "GetPlayerOfficialInfo", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("accountid", accountId32.ToString());
            return (await GetModelAsync<IReadOnlyDictionary<string, FantasyPlayerOfficialInfo>>(cToken: cToken)
                .ConfigureAwait(false))["result"];
        }

        [Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccountAsync) Figure out response object model
        public async Task<ushort> GetEventStatsForAccountAsync(uint steamId32, uint eventId, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IECONDOTA, "GetEventStatsForAccount", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("accountid", steamId32.ToString())
                .AddQuery("eventid", eventId.ToString());
            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken).ConfigureAwait(false));
            return response.result.event_points;
        }

        [Obsolete("This method is incomplete")] // TODO: (GetTopLiveEventGamesAsync) Figure out response object model
        public async Task<IReadOnlyCollection<LiveMatch>> GetTopLiveEventGamesAsync(string apiInterface = IDOTA2_MATCH,
            int partner = 1, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(apiInterface, "GetTopLiveEventGame", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString());
            return (await GetModelAsync<TopLiveGames>(cToken: cToken).ConfigureAwait(false)).Games;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for weekend
        /// tournament games. Returns json string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetWeekendTourneyGames) Figure out response object model
        public async Task<string> GetWeekendTourneyGames(byte partner = 1, uint homeDivision = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST)
                .SetPath(IDOTA2_MATCH, "GetTopWeekendTourneyGames", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString())
                .AddQuery("home_division", homeDivision.ToString());
            return await GetStringAsync(cToken: cToken).ConfigureAwait(false);
        }
        #endregion
    }
}
