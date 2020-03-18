using Newtonsoft.Json.Linq;
using SteamApi.Responses.Dota;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;
using System.Net.Http;

namespace SteamApi
{
    /// <summary>
    /// Specialized HttpClient for making requests for Valve's Dota 2
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
        /// Sends GET request to https://api.steampowered.com for match history
        /// by sequence number. Returns list of MatchDetails unlike other
        /// match history methods. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        public async Task<MatchHistoryBySeqResponse> GetMatchHistoryBySequenceNumAsync(
            ulong seqNum, string apiInterface = IDOTA2_MATCH, string version = "v1",
            uint count = 50, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetMatchHistoryBySequenceNum", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("start_at_match_seq_num", seqNum.ToString())
                .AppendQuery("matches_requested", count.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            MatchHistoryBySeqResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<MatchHistoryBySeqResponseParent>(url, cToken)
                    .ConfigureAwait(false);
                response = webResponse.Result;   
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for MatchHistory.
        /// Request can be specified by providing optional parameters
        /// and can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="minPlayers">minimum number of human players.</param>
        /// <param name="skillLevel">Ignored if an account id is specified.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="count">Call size match count.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <param name="heroId">Include only match with this hero.</param>
        /// <param name="leagueId">League id. Method returns only matches
        ///  from this league. Only for pro dota.</param>
        /// <param name="id32">Player 32-bit id</param>
        /// <param name="startAtMatchId">Match id where to start</param>
        /// <param name="version">API method version</param>
        /// <returns>MatchHistoryResponse object</returns>
        public async Task<MatchHistoryResponse> GetMatchHistoryAsync(uint id32 = 0, uint heroId = 0,
            uint minPlayers = 0, uint leagueId = 0, ulong startAtMatchId = 0, uint count = 25,
            string apiInterface = IDOTA2_MATCH, string version = "v1",
            DotaSkillLevel skillLevel = DotaSkillLevel.Any, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetMatchHistory", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("account_id", id32.ToString())
                .AppendQuery("matches_requested", count.ToString())
                .AppendQuery("hero_id", heroId.ToString())
                .AppendQuery("min_players", minPlayers.ToString())
                .AppendQuery("league_id", leagueId.ToString())
                .AppendQuery("start_at_match_id", startAtMatchId.ToString())
                .AppendQuery("skill", ((int)skillLevel).ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            MatchHistoryResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<MatchHistoryResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                response = webResponse.Result;

                // Set skill level to each match model
                foreach (var match in response.Contents)
                {
                    match.SkillLevel = (int)skillLevel;
                }
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// Dota 2 match details. Request can be cancelled by
        /// providing cancellation token.
        /// </summary>
        /// <param name="matchId">Dota 2 match id</param>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        public async Task<MatchDetailsResponse> GetMatchDetailsAsync(ulong matchId,
            string apiInterface = IDOTA2_MATCH, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetMatchDetails", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("match_id", matchId.ToString())
                .AppendQuery("include_persona_names", "1");

            string url = UrlBuilder.PopEncodedUrl(false);
            MatchDetailsResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                response = await GetModelAsync<MatchDetailsResponse>(url, cToken)
                    .ConfigureAwait(false);
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <param name="partner">Partner id. Usually 0-3 works.</param>
        public async Task<TopLiveGamesResponse> GetTopLiveGamesAsync(string apiInterface = IDOTA2_MATCH,
            string version = "v1", uint partner = 0, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTopLiveGame", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("partner", partner.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            TopLiveGamesResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<TopLiveGamesResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Contents == null)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }

        #endregion

        #region [Game Constant Data]

        /// <summary>
        /// Sends GET request to https://dota2.com for dota heroinfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        public async Task<HeroInfoResponse> GetHeroInfosAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "heropickerdata");

            string url = UrlBuilder.PopEncodedUrl(false);
            HeroInfoResponse response = null;
            Exception thrownException = null;
            try
            {
                var webResponse = await GetModelAsync<IReadOnlyDictionary<string, HeroInfo>>(url, cToken)
                    .ConfigureAwait(false);
                if (webResponse.Count == 0)
                    throw new ApiEmptyResultException("Web API response was empty");
                else
                    response = new HeroInfoResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://dota2.com for dota 2
        /// hero stats. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        public async Task<HeroStatsResponse> GetHeroStatsAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "heropediadata");
            UrlBuilder.AppendQuery("feeds", "herodata");

            string url = UrlBuilder.PopEncodedUrl(false);
            HeroStatsResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<HeroStatsResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Contents.Count == 0)
                    throw new ApiEmptyResultException("Web response didn't return any values");
                else 
                    response = webResponse;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com
        /// for dota 2 heroes. Request can be cancelled by
        /// providing cancellation token.
        /// </summary>
        /// <param name="lang">Language</param>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        public async Task<HeroesResponse> GetHeroesAsync(string lang = "en",
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetHeroes", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("language", lang);

            string url = UrlBuilder.PopEncodedUrl(false);
            Exception thrownException = null;
            HeroesResponse response = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<HeroesResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Content.Contents.Count == 0)
                    throw new ApiEmptyResultException("Web response didn't return any values");
                else
                    response = webResponse.Content;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to dota2.com for Dota 2 iteminfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Item>> GetItemInfosAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "itemdata");



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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetGameItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("language", lang);

            var response = await GetModelAsync<GameItems>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Items;
        }

        /// <summary>
        /// Sends GET request for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        public async Task<IReadOnlyDictionary<string, Ability>> GetAbilitiesAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "abilitydata");

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
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "uniqueusers");

            return await GetModelAsync<Dictionary<string, uint>>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 leaderboards. Specify
        /// region. Request can be cancelled by providing cancellation token.
        /// </summary>
        public async Task<Leaderboard> GetLeaderboardAsync(DotaRegion region = default,
            string version = "v0001", CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "ILeaderboard", "GetDivisionLeaderboard", version);

            switch (region)
            {
                case DotaRegion.Europe:
                    UrlBuilder.AppendQuery("division", "europe");
                    break;
                case DotaRegion.America:
                    UrlBuilder.AppendQuery("division", "americas");
                    break;
                case DotaRegion.SEA:
                    UrlBuilder.AppendQuery("division", "se_asia");
                    break;
                case DotaRegion.China:
                    UrlBuilder.AppendQuery("division", "china");
                    break;
            }
            return await GetModelAsync<Leaderboard>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 player profile.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="id32">32-bit steam id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <exception cref="ApiEmptyResultException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<DotaPlayerProfile> GetPlayerProfileAsync(uint id32,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetPlayerInfo", version);
            UrlBuilder.AppendQuery("account_id", id32.ToString());

            var response = await GetModelAsync<DotaPlayerProfile>(cToken: cToken)
                .ConfigureAwait(false);

            if (response.Id32 != id32)
                throw new ApiEmptyResultException("Requested profile is not visible to you");
            else
                return response;
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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetEquippedPlayerItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamid", steamId64.ToString())
                .AppendQuery("class_id", heroClassId.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetPlayerItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamid", steamid64.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA_BETA, "GetItemIconPath", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("iconname", iconName)
                .AppendQuery("icontype", iconType.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetSchemaURL", version);
            UrlBuilder.AppendQuery("key", ApiKey);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetStoreMetaData", version);
            UrlBuilder.AppendQuery("key", ApiKey);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA, "GetItemCreators", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("itemdef", itemDef.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetRarities", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("language", lang);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTournamentPlayerStats", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("account_id", accountId32.ToString())
                .AppendQuery("league_id", leagueId32.ToString())
                .AppendQuery("hero_id", heroId.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_MATCH_STATS, "GetRealtimeStats", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("server_steam_id", serverId);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_MATCH, "GetLiveLeagueGames", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("match_id", matchId64.ToString())
                .AppendQuery("league_id", leagueId32.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath("IDOTA2Match_205790", "GetLeagueListing", version);
            UrlBuilder.AppendQuery("key", ApiKey);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTournamentPrizePool", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("leagueid", leagueId.ToString());

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
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetLeagueNodeData", version);
            UrlBuilder.AppendQuery("league_id", leagueId.ToString())
                .AppendQuery("node_id", nodeId.ToString());

            return await GetModelAsync<LeagueNode>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for tournament infos beginning from provided
        /// Unix Timestamp. If Unix Timestamp is not provided request uses
        /// current timestamp. Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="timestamp">Query startime as unix timestamp</param>
        /// <param name="version">API method version</param>
        /// <param name="minTier">min tier included</param>
        /// <param name="cToken">Cancellation token</param>
        public async Task<IReadOnlyList<TournamentInfo>> GetTournamentInfoAsync(long timestamp = -1,
            string version = "v1", uint minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(version, ValidateTimestamp(timestamp), minTier);

            var response = await GetModelAsync<TournamentInfoCollection>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Infos;
        }

        /// <summary>
        /// Sends GET request for tournament infos beginnning
        /// from provided date.Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="datetime">Query startime</param>
        /// <param name="version">API method version</param>
        /// <param name="minTier">min tier included</param>
        /// <param name="cToken">Cancellation token</param>
        public async Task<IReadOnlyList<TournamentInfo>> GetTournamentInfoAsync(DateTime datetime,
            string version = "v1", uint minTier = 1, CToken cToken = default)
        {
            CreateUrlForTournamentInfo(version, GetUnixTimestampFromDate(datetime), minTier);

            var response = await GetModelAsync<TournamentInfoCollection>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Infos;
        }

        /// <summary>
        /// Creates url for GetTournamentInfoAsync() methods
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <param name="minTier">min tier in query</param>
        /// <param name="version">API method version</param>
        /// <returns>UrlBuilder object that contains the url.</returns>
        private void CreateUrlForTournamentInfo(string version, ulong timestamp, uint minTier)
        {
            if (minTier > 5)
            {
                throw new ArgumentException("Minimum tier can't be larger 5");
            }
            else
            {
                UrlBuilder.Host = DOTA_2_HOST;
                UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetLeagueInfoList", version);
                UrlBuilder.AppendQuery("start_timestamp", timestamp.ToString())
                    .AppendQuery("min_tier", minTier.ToString());
            }
        }

        /// <summary>
        /// Sends GET request for Internation pricepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        public async Task<IReadOnlyDictionary<string, uint>> GetInternationalPrizePoolAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "intlprizepool");

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
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetRecentAndUpcomingMatches", version);

            return await GetModelAsync<RecentDcpEvents>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 team info. DCP results
        /// can be included to request. Request can be cancelled by provding
        /// cancellation token.
        /// </summary>
        public async Task<DotaTeam> GetDotaTeamAsync(ulong teamId, string version = "v1",
            bool includeDCP = false, CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetSingleTeamInfo", version);
            UrlBuilder.AppendQuery("team_id", teamId.ToString());
            if (includeDCP)
                UrlBuilder.AppendQuery("get_dpc_info", "1");

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTeamInfoByTeamID", "v1");
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("start_at_team_id", startId.ToString())
                .AppendQuery("teams_requested", count.ToString());

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
            UrlBuilder.Host = STEAM_MEDIA_HOST;
            switch (imgShape)
            {
                case HeroImageShape.Vertical:
                    UrlBuilder.AppendPath("apps", "dota2", "images", "heroes", heroName + "_vert.jpg");
                    break;
                case HeroImageShape.Full:
                    UrlBuilder.AppendPath("apps", "dota2", "images", "heroes", heroName + "_full.png");
                    break;
                case HeroImageShape.Horizontal:
                    UrlBuilder.AppendPath("apps", "dota2", "images", "heroes", heroName + "_lg.png");
                    break;
                case HeroImageShape.Small:
                    UrlBuilder.AppendPath("apps", "dota2", "images", "heroes", heroName + "_sb.png");
                    break;
            }

            return await GetBytesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for item image.
        /// </summary>
        public async Task<byte[]> GetItemImageAsync(string imgName, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_MEDIA_HOST;
            UrlBuilder.AppendPath("apps", "dota2", "images", "items", imgName);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_TICKET, "SteamAccountValidForBadgeType", version);
            UrlBuilder.AppendQuery("steamid", steamid64.ToString())
                .AppendQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AppendQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AppendQuery("ValidBadgeType3", validBadgeType3.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_TICKET, "ClaimBadgeReward", version);
            UrlBuilder.AppendQuery("BadgeID", badgeId)
                .AppendQuery("ValidBadgeType1", validBadgeType1.ToString())
                .AppendQuery("ValidBadgeType2", validBadgeType2.ToString())
                .AppendQuery("ValidBadgeType3", validBadgeType3.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_TICKET, "GetSteamIDForBadgeID", version);
            UrlBuilder.AppendQuery("BadgeID", badgeId);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA_BETA, "GetEventStatsForAccount", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("eventid", eventId.ToString())
                .AppendQuery("accountid", accountId32.ToString())
                .AppendQuery("language", lang);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_FANTASY, "GetProPlayerList", version);
            UrlBuilder.AppendQuery("key", ApiKey);

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_FANTASY, "GetFantasyPlayerStats", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("StartTime", startTimestamp)
                .AppendQuery("EndTime", endTimestamp)
                .AppendQuery("MatchID", matchId.ToString())
                .AppendQuery("SeriesID", seriesId.ToString())
                .AppendQuery("PlayerAccountID", playerId32.ToString())
                .AppendQuery("FantasyLeagueID", fantasyLeagueId.ToString());

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
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_FANTASY, "GetPlayerOfficialInfo", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("accountid", accountId32.ToString());

            return (await GetModelAsync<IReadOnlyDictionary<string, FantasyPlayerOfficialInfo>>(cToken: cToken)
                .ConfigureAwait(false))["result"];
        }

        [Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccountAsync) Figure out response object model
        public async Task<ushort> GetEventStatsForAccountAsync(uint steamId32, uint eventId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA, "GetEventStatsForAccount", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("accountid", steamId32.ToString())
                .AppendQuery("eventid", eventId.ToString());

            dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false));

            return response.result.event_points;
        }

        [Obsolete("This method is incomplete")] // TODO: (GetTopLiveEventGamesAsync) Figure out response object model
        public async Task<IReadOnlyList<LiveMatch>> GetTopLiveEventGamesAsync(string apiInterface = IDOTA2_MATCH,
            string version = "v1", int partner = 1, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTopLiveEventGame", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("partner", partner.ToString());

            var response = await GetModelAsync<TopLiveGamesResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Contents;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for weekend
        /// tournament games. Returns json string.
        /// </summary>
        [Obsolete("This method is incomplete")] // TODO: (GetWeekendTourneyGames) Figure out response object model
        public async Task<string> GetWeekendTourneyGames(int partner = 1,
            string version = "v1", uint homeDivision = 0, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_MATCH, "GetTopWeekendTourneyGames", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("partner", partner.ToString())
                .AppendQuery("home_division", homeDivision.ToString());

            return await GetStringAsync(cToken: cToken)
                .ConfigureAwait(false);
        }
        #endregion
    }
}
