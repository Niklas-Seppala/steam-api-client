using Newtonsoft.Json.Linq;
using SteamApi.Models.Dota;
using SteamApi.Models.Dota.ResponseModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApi
{
    /// <summary>
    /// Special HttpClient for making requests for Valve's Dota 2
    /// APIs. Derived from ApiClient class.
    /// </summary>
    public class DotaApiClient : ApiClient
    {
        /// <summary>
        /// DotaApiClient's URL to send test request.
        /// </summary>
        /// <see cref="ApiClient.TestUrl"/>
        protected override string TestUrl => $"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1?key={ApiKey}";

        #region [Url Constants]

        // API hosts
        private const string STEAM_HOST = "api.steampowered.com";
        private const string DOTA_2_HOST = "www.dota2.com";
        private const string STEAM_MEDIA_HOST = "media.steampowered.com";
        
        // API interfaces
        private const string IDOTA2_FANTASY = "IDOTA2Fantasy_205790";
        private const string IDOTA2_MATCH_STATS = "IDOTA2MatchStats_570";
        private const string IDOTA2_MATCH = "IDOTA2Match_570";
        private const string IDOTA2_TICKET = "IDOTA2Ticket_570";
        private const string IECONDOTA_BETA = "IEconDOTA2_205790";
        private const string IECONDOTA = "IEconDOTA2_570";
        private const string IECON_ITEMS = "IEconItems_570";

        #endregion

        /// <summary>
        /// Instantiates DotaApiClient object.
        /// </summary>
        /// <param name="testConnection">default is false</param>
        /// <param name="schema">default is "https"</param>
        /// <see cref="ApiClient(bool, string)"/>
        public DotaApiClient(bool testConnection = false, string schema = "https") : base(testConnection, schema)
        {
            // Initialization happens in parent class's constructor.
        }

        #region [Matches]

        /// <summary>
        /// Sends GET request to api.steampowered.com for match history
        /// by sequence number. Default interface is IDOTA2Match_570.
        /// Returns list of ! MatchDetails ! unlike other match history methods.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<MatchDetails>> GetMatchHistoryBySequenceNumAsync(
            ulong seqNum, string apiInterface = IDOTA2_MATCH, string version = "v1",
            uint count = 50, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetMatchHistoryBySequenceNum", version)
                .AddQuery("key", ApiKey)
                .AddQuery("start_at_match_seq_num", seqNum.ToString())
                .AddQuery("matches_requested", count.ToString());

            var response = await GetModelAsync<MatchHistoryBySeqResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result.Matches;
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
            string apiInterface = IDOTA2_MATCH, string version = "v1", uint skillLevel = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetMatchHistory", version)
                .AddQuery("key", ApiKey)
                .AddQuery("account_id", playerId32.ToString())
                .AddQuery("matches_requested", count.ToString())
                .AddQuery("hero_id", heroId.ToString())
                .AddQuery("min_players", minPlayers.ToString())
                .AddQuery("league_id", leagueId.ToString())
                .AddQuery("start_at_match_id", startAtId.ToString())
                .AddQuery("skill", skillLevel.ToString());

            var response = await GetModelAsync<MatchHistoryContainer>(cToken: cToken)
                .ConfigureAwait(false);

            return response.History;
        }

        /// <summary>
        /// Sends GET request for Dota 2 match details. Request can be
        /// cancelled by providing cancellation token.
        /// </summary>
        public async Task<MatchDetails> GetMatchDetailsAsync(string matchId,
            string apiInterface = IDOTA2_MATCH, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST).SetPath(apiInterface, "GetMatchDetails", version)
               .AddQuery("key", ApiKey)
               .AddQuery("match_id", matchId.ToString())
               .AddQuery("include_persona_names", "1");

            var response = await GetModelAsync<MatchDetailsContainer>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Details;
        }

        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="partner">partner id</param>
        public async Task<IReadOnlyList<LiveMatch>> GetTopLiveGamesAsync(string apiInterface = IDOTA2_MATCH,
            string version = "v1", int partner = 1, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetTopLiveGame", version)
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString());

            var response = await GetModelAsync<TopLiveGames>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Games;
        }

        #endregion

        #region [Game Constant Data]
        /// <summary>
        /// Sends GET request for dota heroinfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, HeroInfo>> GetHeroInfosAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("jsfeed", "heropickerdata");

            return await GetModelAsync<IReadOnlyDictionary<string, HeroInfo>>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 hero stats. Requst
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, HeroStats>> GetHeroStatsAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("jsfeed", "heropediadata")
                .AddQuery("feeds", "herodata");

            var response = await GetModelAsync<HeroStatsContainer>(cToken: cToken)
                .ConfigureAwait(false);

            return response.HeroStats;
        }

        /// <summary>
        /// Sends GET request for dota 2 heroes. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<Hero>> GetHeroesAsync(string lang = "en",
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetHeroes", version)
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);

            var response = await GetModelAsync<HeroesResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Heroes;
        }

        /// <summary>
        /// Sends GET request to dota2.com for Dota 2 iteminfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Item>> GetItemInfoDictAsync(CToken cToken = default)
        {
            UrlBuilder.SetPath(DOTA_2_HOST)
                .SetPath("jsfeed", "itemdata");

            var response = await GetModelAsync<ItemDictionary>(cToken: cToken)
                .ConfigureAwait(false);

            return response.ItemDict;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for dota 2 items. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<Item>> GetGameItemsAsync(string lang = "en",
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetGameItems", version)
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);

            var response = await GetModelAsync<GameItems>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Items;
        }

        /// <summary>
        /// Sends GET request for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Ability>> GetAbilitiesDictAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("jsfeed", "abilitydata");

            var response = await GetModelAsync<AbilitiesResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.AbilityDict;
        }
        #endregion

        #region [Users]
        /// <summary>
        /// Sends GET request for unique user count. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, uint>> GetUniqueUsersAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("jsfeed", "uniqueusers");

            return await GetModelAsync<Dictionary<string, uint>>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 leaderboards. Specify
        /// region. Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<Leaderboard> GetLeaderboardAsync(DotaRegion region = default,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("webapi", "ILeaderboard", "GetDivisionLeaderboard", version);

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
            return await GetModelAsync<Leaderboard>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 player profile.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<DotaPlayerProfile> GetDotaPlayerProfileAsync(string id32,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("webapi", "IDOTA2DPC", "GetPlayerInfo", version)
                .AddQuery("account_id", id32);

            return await GetModelAsync<DotaPlayerProfile>(cToken: cToken)
                .ConfigureAwait(false);
        }
        #endregion

        #region [Cosmetic Items]
        /// <summary>
        /// Sends GET request to api.steampowered.com for players equiped
        /// items for specified hero. Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<EquipedItem>> GetEquipedPlayerItemsAsync(ulong steamId64,
            ushort heroClassId, string apiInterface = IECON_ITEMS, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetEquippedPlayerItems", version)
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
            string apiInterface = IECON_ITEMS, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetPlayerItems", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamid", steamid64.ToString());

            var response = await GetModelAsync<IReadOnlyDictionary<string, PlayerInventory>>(cToken: cToken)
                .ConfigureAwait(false);

            return response["result"];
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// item icon path.
        /// </summary>
        /// <param name="iconName">the item icon name to get the CDN path of</param>
        /// <param name="iconType">the type of image you want. 0 = normal, 1 = large, 2 = ingame</param>
        public async Task<string> GetItemIconPathAsync(string iconName, byte iconType = 0,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IECONDOTA_BETA, "GetItemIconPath", version)
                .AddQuery("key", ApiKey)
                .AddQuery("iconname", iconName)
                .AddQuery("icontype", iconType.ToString());

            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false));

            return response.result.path;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for item schema url.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiInterface">api interface</param>
        public async Task<string> GetSchemaUrlAsync(string apiInterface = IECON_ITEMS,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetSchemaURL", version)
                .AddQuery("key", ApiKey);

            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false));

            return response.result.items_game_url;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for strore metadata.
        /// Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<StoreMetadata> GetStoreMetadataAsync(string apiInterface = IECON_ITEMS,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetStoreMetaData", version)
                .AddQuery("key", ApiKey);

            var response = await GetModelAsync<IReadOnlyDictionary<string, StoreMetadata>>(cToken: cToken)
                .ConfigureAwait(false);

            return response["result"];
        }

        /// <summary>
        /// Sends GET request for item creator ids for
        /// specified item. Request can be cancelled
        /// by providing Cancellation token.
        /// </summary>
        /// <param name="itemDef">item id</param>
        public async Task<IReadOnlyList<uint>> GetItemCreatorsAsync(uint itemDef,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IECONDOTA, "GetItemCreators", version)
                .AddQuery("key", ApiKey)
                .AddQuery("itemdef", itemDef.ToString());

            var creators = await GetModelAsync<ItemCreators>(cToken: cToken)
                .ConfigureAwait(false);

            return creators.Contributors;
        }

        /// <summary>
        /// Sends GET request for Dota 2 cosmetic item rarities.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<DotaCosmeticRarity>> GetCosmeticRaritiesAsync(CToken cToken = default,
            string apiInterface = IECONDOTA, string version = "v1",  string lang = "en")
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetRarities", version)
                .AddQuery("key", ApiKey)
                .AddQuery("language", lang);

            var result = await GetModelAsync<DotaCosmeticRaritiesResult>(cToken: cToken)
                .ConfigureAwait(false);

            return result.Result.Rarities;
        }
        #endregion

        #region [Get Tournament Data]

        /// <summary>
        /// Sends GET request to api.steampowered.com for players tournament
        /// stats. Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<TournamentPlayerStats> GetTournamentPlayerStatsAsync(uint accountId32, uint leagueId32,
            string apiInterface = IDOTA2_MATCH, string version = "v2", ushort heroId = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetTournamentPlayerStats", version)
                .AddQuery("key", ApiKey)
                .AddQuery("account_id", accountId32.ToString())
                .AddQuery("league_id", leagueId32.ToString())
                .AddQuery("hero_id", heroId.ToString());

            var response = await GetModelAsync<TournamentPlayerStatsResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for real time match stats.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<RealtimeMatchStats> GetRealtimeMatchStatsAsync(string serverId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_MATCH_STATS, "GetRealtimeStats", version)
                .AddQuery("key", ApiKey)
                .AddQuery("server_steam_id", serverId);

            return await GetModelAsync<RealtimeMatchStats>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for live
        /// league games. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<LiveLeagueMatch>> GetLiveLeagueMatchAsync(string version = "v1",
            ulong matchId64 = 0, uint leagueId32 = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_MATCH, "GetLiveLeagueGames", version)
                .AddQuery("key", ApiKey)
                .AddQuery("match_id", matchId64.ToString())
                .AddQuery("league_id", leagueId32.ToString());

            var response = await GetModelAsync<LiveLeagueMatchResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result.Games;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for leaguelistings.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<League>> GetLeagueListingAsync(string version = "v1",
            CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath("IDOTA2Match_205790", "GetLeagueListing", version)
                .AddQuery("key", ApiKey);

            var response = await GetModelAsync<LeagueListingResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result.Leagues;
        }

        /// <summary>
        /// Sends GET request for dota 2 tournament prizepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, uint>> GetTournamentPrizePoolAsync(uint leagueId,
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetTournamentPrizePool", version)
                .AddQuery("key", ApiKey)
                .AddQuery("leagueid", leagueId.ToString());

            var response = await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyDictionary<string, uint>>>(cToken: cToken)
                .ConfigureAwait(false);

            return response["result"];
        }

        /// <summary>
        /// Sends GET request for league node data.
        /// Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        public async Task<LeagueNode> GetLeagueNodeAsync(ulong leagueId, ulong nodeId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("webapi", "IDOTA2DPC", "GetLeagueNodeData", version)
                .AddQuery("league_id", leagueId.ToString())
                .AddQuery("node_id", nodeId.ToString());

            return await GetModelAsync<LeagueNode>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for tournament infos beginning from provided
        /// Unix Timestamp. If Unix Timestamp is not provided request uses
        /// current timestamp. Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<TournamentInfo>> GetTournamentInfoAsync(long timestamp = -1,
            string version = "v1", byte maxTier = 5, byte minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(version, ValidateTimestamp(timestamp), maxTier, minTier);

            var response = await GetModelAsync<TournamentInfoCollection>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Infos;
        }

        /// <summary>
        /// Sends GET request for tournament infos beginnning
        /// from provided date.Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<TournamentInfo>> GetTournamentInfoAsync(DateTime datetime,
            string version = "v1", byte maxTier = 5, byte minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(version, GetUnixTimestampFromDate(datetime), maxTier, minTier);

            var response = await GetModelAsync<TournamentInfoCollection>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Infos;
        }

        /// <summary>
        /// Creates url for GetTournamentInfoAsync() methods
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <returns>UrlBuilder object that contains the url.</returns>
        private void CreateUrlForTournamentInfo(string version, ulong timestamp, byte maxTier, byte minTier)
        {
            if (minTier > maxTier)
            {
                throw new ArgumentException("Minimum tier can't be larger than maximum tier");
            }
            else
            {
                UrlBuilder.SetHost(DOTA_2_HOST)
                    .SetPath("webapi", "IDOTA2DPC", "GetLeagueInfoList", version)
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
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("jsfeed", "intlprizepool");

            return await GetModelAsync<IReadOnlyDictionary<string, uint>>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for recent and upcoming DCP
        /// events. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<RecentDcpEvents> GetRecentDcpEventsAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("webapi", "IDOTA2DPC", "GetRecentAndUpcomingMatches", version);
            return await GetModelAsync<RecentDcpEvents>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 team info. DCP results
        /// can be included to request. Request can be cancelled by provding
        /// cancellation token.
        /// </summary>
        public async Task<DotaTeam> GetDotaTeamAsync(string teamId, string version = "v1",
            bool includeDCP = false, CToken cToken = default)
        {
            UrlBuilder.SetHost(DOTA_2_HOST)
                .SetPath("webapi", "IDOTA2DPC", "GetSingleTeamInfo", version)
                .AddQuery("team_id", teamId);
            if (includeDCP)
                UrlBuilder.AddQuery("get_dpc_info", "1");

            return await GetModelAsync<DotaTeam>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for DotaTeamInfos by providing start team
        /// id. Default request size is 100 teams. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyList<DotaTeamInfo>> GetDotaTeamInfosByIdAsync(ulong startId = 1,
            uint count = 100, string apiInterface = IDOTA2_MATCH, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetTeamInfoByTeamID", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("start_at_team_id", startId.ToString())
                .AddQuery("teams_requested", count.ToString());

            var response = await GetModelAsync<DotaTeamInfosResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result.Teams;
        }

        #endregion

        #region [Images]

        /// <summary>
        /// Sends GET request for hero image.
        /// </summary>
        public async Task<byte[]> GetHeroImageAsync(string heroName, HeroImageShape imgShape = HeroImageShape.Horizontal)
        {
            UrlBuilder.SetHost(STEAM_MEDIA_HOST);
                
            switch (imgShape)
            {
                case HeroImageShape.Vertical:
                    UrlBuilder.SetPath("apps", "dota2", "images", "heroes", heroName + "_vert.jpg");
                    break;
                case HeroImageShape.Full:
                    UrlBuilder.SetPath("apps", "dota2", "images", "heroes", heroName + "_full.png");
                    break;
                case HeroImageShape.Horizontal:
                    UrlBuilder.SetPath("apps", "dota2", "images", "heroes", heroName + "_lg.png");
                    break;
                case HeroImageShape.Small:
                    UrlBuilder.SetPath("apps", "dota2", "images", "heroes", heroName + "_sb.png");
                    break;
            }

            return await GetBytesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for item image.
        /// </summary>
        public async Task<byte[]> GetItemImageAsync(string imgName, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_MEDIA_HOST)
                .SetPath("apps", "dota2", "images", "items", imgName);

            return await GetBytesAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        #endregion

        #region [Unfinished] 
        /// <summary>
        /// I really don't know what this API method does :). In case
        /// you do, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetSteamAccountValidForBadgeType) Figure out what this method does!
        public async Task<string> GetSteamAccountValidForBadgeType(ulong steamid64, uint validBadgeType1,
            uint validBadgeType2, uint validBadgeType3, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_TICKET, "SteamAccountValidForBadgeType", version)
                .AddQuery("steamid", steamid64.ToString())
                .AddQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AddQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AddQuery("ValidBadgeType3", validBadgeType3.ToString());

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// I really don't know what this API method does :). In case
        /// you do, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (ClaimBadgeReward) Figure out what this method does!
        public async Task<string> ClaimBadgeReward(string badgeId, uint validBadgeType1,
            uint validBadgeType2, uint validBadgeType3, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_TICKET, "ClaimBadgeReward", version)
                .AddQuery("BadgeID", badgeId)
                .AddQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AddQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AddQuery("ValidBadgeType3", validBadgeType3.ToString());

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// I really don't know what this API method does :). In case
        /// you do, returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetSteamIDForBadgeId) Figure out what this method does!
        public async Task<string> GetSteamIDForBadgeId(string badgeId, string version = "v1",
            CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_TICKET, "GetSteamIDForBadgeID", version)
                .AddQuery("BadgeID", badgeId);

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// event stats for specified account. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccount) Figure out response object model
        public async Task<string> GetEventStatsForAccount(uint eventId, uint accountId32,
            string version = "v1", string lang = "en", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IECONDOTA_BETA, "GetEventStatsForAccount", version)
                .AddQuery("key", ApiKey)
                .AddQuery("eventid", eventId.ToString())
                .AddQuery("accountid", accountId32.ToString())
                .AddQuery("language", lang);

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// Fantasy pro player listing. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetProPlayerList) Figure out response object model
        public async Task<string> GetProPlayerList(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_FANTASY, "GetProPlayerList", version)
                .AddQuery("key", ApiKey);

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered for fantasy
        /// player stats. Returns JSON string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetFantasyPlayerStats) Figure out response object model
        public async Task<string> GetFantasyPlayerStats(uint fantasyLeagueId, string version = "v1",
            string startTimestamp = "0", string endTimestamp = "0", ulong matchId = 0, uint seriesId = 0,
            uint playerId32 = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_FANTASY, "GetFantasyPlayerStats", version)
                .AddQuery("key", ApiKey)
                .AddQuery("StartTime", startTimestamp)
                .AddQuery("EndTime", endTimestamp)
                .AddQuery("MatchID", matchId.ToString())
                .AddQuery("SeriesID", seriesId.ToString())
                .AddQuery("PlayerAccountID", playerId32.ToString())
                .AddQuery("FantasyLeagueID", fantasyLeagueId.ToString());

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// pro players official fantasy info. Request can
        /// be cancelled by providing cancellation token.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetPlayerOfficialFantasyInfo) Figure out response object model
        public async Task<FantasyPlayerOfficialInfo> GetPlayerOfficialFantasyInfo(
            uint accountId32, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_FANTASY, "GetPlayerOfficialInfo", version)
                .AddQuery("key", ApiKey)
                .AddQuery("accountid", accountId32.ToString());

            return (await GetModelAsync<IReadOnlyDictionary<string, FantasyPlayerOfficialInfo>>(cToken: cToken)
                .ConfigureAwait(false))["result"];
        }

        [Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccountAsync) Figure out response object model
        public async Task<ushort> GetEventStatsForAccountAsync(uint steamId32, uint eventId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IECONDOTA, "GetEventStatsForAccount", version)
                .AddQuery("key", ApiKey)
                .AddQuery("accountid", steamId32.ToString())
                .AddQuery("eventid", eventId.ToString());

            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false));

            return response.result.event_points;
        }

        [Obsolete("This method is incomplete")] // TODO: (GetTopLiveEventGamesAsync) Figure out response object model
        public async Task<IReadOnlyList<LiveMatch>> GetTopLiveEventGamesAsync(string apiInterface = IDOTA2_MATCH,
            string version = "v1", int partner = 1, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(apiInterface, "GetTopLiveEventGame", version)
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString());

            var response = await GetModelAsync<TopLiveGames>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Games;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for weekend
        /// tournament games. Returns json string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetWeekendTourneyGames) Figure out response object model
        public async Task<string> GetWeekendTourneyGames(int partner = 1,
            string version = "v1", uint homeDivision = 0, CToken cToken = default)
        {
            UrlBuilder.SetHost(STEAM_HOST)
                .SetPath(IDOTA2_MATCH, "GetTopWeekendTourneyGames", version)
                .AddQuery("key", ApiKey)
                .AddQuery("partner", partner.ToString())
                .AddQuery("home_division", homeDivision.ToString());

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }
        #endregion
    }
}
