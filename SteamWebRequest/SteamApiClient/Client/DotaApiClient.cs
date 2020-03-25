using Newtonsoft.Json.Linq;
using SteamApi.Responses.Dota;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

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
        /// <param name="seqNum">Match sequence number where to start,</param>
        /// <param name="count">Match count.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of dota 2 match details wrapped into ApiResponse object.</returns>
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

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
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
        /// <returns>List of Dota 2 matches wrapped into ApiResponse object.</returns>
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

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                {
                    response = webResponse.Result;

                    // Set skill level to each match model
                    foreach (var match in response.Contents)
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
        /// <returns>Dota 2 match details wrapped into ApiResponse object.</returns>
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

                var webResponse = await GetModelAsync<MatchDetailsResponse>(url, cToken)
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


        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <param name="partner">Partner id. Usually 0-3 works.</param>
        /// <returns>List of top live dota 2 games wrapped into ApiResponse object.</returns>
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

                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
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
        /// <returns>Dota 2 hero info dictionary wrapped into ApiResponse object.</returns>
        public async Task<HeroInfoResponse> GetHeroInfosAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "heropickerdata");

            string url = UrlBuilder.PopEncodedUrl(false);
            HeroInfoResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<IReadOnlyDictionary<string, HeroInfo>>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse == null || webResponse.Count == 0)
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
        /// <returns>Dota 2 hero stats dictionary wrapped into ApiResponse object</returns>
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

                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
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
        /// <returns>Dota 2 heroes list wrapped into ApiResponse object.</returns>
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

                if (webResponse.Content.Contents == null || webResponse.Content.Contents.Count == 0)
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
        /// Sends GET request to https://dota2.com for Dota 2 iteminfo.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Dictionary of dota 2 items wrapped into ApiResponse object</returns>
        public async Task<ItemsInfoResponse> GetItemInfosAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "itemdata");

            string url = UrlBuilder.PopEncodedUrl(false);
            ItemsInfoResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<ItemsInfoResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
                    throw new ApiEmptyResultException("Web response didn't return any values");
                else
                    response = webResponse;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }

        /// <summary>
        /// Sends GET request to https://api.steampowered.com for dota 2 items.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="lang">Language.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of dota 2 items wrapped into ApiResponse object</returns>
        public async Task<ItemsResponse> GetItemsAsync(string lang = "en",
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetGameItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("language", lang);

            string url = UrlBuilder.PopEncodedUrl(false);
            ItemsResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<ItemsResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse.Result;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }

        /// <summary>
        /// Sends GET request to https://dota2.com for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Dictionary of abilities wrapped into ApiResponse object</returns>
        public async Task<AbilitiesResponse> GetAbilitiesAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "abilitydata");

            string url = UrlBuilder.PopEncodedUrl(false);
            AbilitiesResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<AbilitiesResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }

        #endregion

        #region [Users]

        /// <summary>
        /// Sends GET request for unique user count. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        public async Task<UniqueUsersResponse> GetUniqueUsersAsync(CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "uniqueusers");

            string url = UrlBuilder.PopEncodedUrl(false);
            UniqueUsersResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<Dictionary<string, uint>>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse == null || webResponse.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = new UniqueUsersResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }

        /// <summary>
        /// Sends GET request to https://www.dota2.com for dota 2
        /// leaderboards. Specify region. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="region">Dota 2 region.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>Dota 2 regional leaderboard wrapped into ApiResponse object.</returns>
        public async Task<LeaderboardResponse> GetLeaderboardAsync(DotaRegion region = default,
            string apiInterface = "ILeaderboard", string version = "v0001", CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", apiInterface, "GetDivisionLeaderboard", version);
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

            string url = UrlBuilder.PopEncodedUrl(false);
            LeaderboardResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<Leaderboard>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Players == null || webResponse.Players.Count == 0)
                    throw new ApiEmptyResultException("");
                else
                    response = new LeaderboardResponse() { Contents = webResponse };
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://www.dota2.com for
        /// dota 2 player profile. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="id32">32-bit steam id.</param>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>DotaPlayerProfile wrapped into ApiResponse object.</returns>
        public async Task<DotaPlayerProfileResponse> GetPlayerProfileAsync(uint id32,
            string apiInterface = "IDOTA2DPC", string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", apiInterface, "GetPlayerInfo", version);
            UrlBuilder.AppendQuery("account_id", id32.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            DotaPlayerProfileResponse response = null;
            Exception caughtException = null;

            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaPlayerProfile>(url, cToken)
                    .ConfigureAwait(false);
                if (string.IsNullOrEmpty(webResponse.Name))
                    throw new ApiEmptyResultException("API response was empty.");
                else if (webResponse.Id32 != id32)
                    throw new ApiEmptyResultException("Requested profile is not visible to you.");
                else
                    response = new DotaPlayerProfileResponse() { Contents = webResponse };
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }

        #endregion

        #region [Cosmetic Items]

        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// players equiped items for specified hero. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="steamId64">Player's 64-bit steam id.</param>
        /// <param name="heroClassId">Id of the hero.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of equiped hero items wrapped into ApiResponse object.</returns>
        public async Task<EquipedItemsResponse> GetEquipedPlayerItemsAsync(ulong steamId64,
            uint heroClassId, string apiInterface = IECON_ITEMS, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetEquippedPlayerItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamid", steamId64.ToString())
                .AppendQuery("class_id", heroClassId.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            EquipedItemsResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<IReadOnlyDictionary<string,
                    EquipedCosmeticItems>>(url, cToken).ConfigureAwait(false);

                if (webResponse.TryGetValue("result", out EquipedCosmeticItems items))
                {
                    if (items == null || items.Items.Count == 0)
                        throw new ApiEmptyResultException("API response was empty.");
                    else
                        response = new EquipedItemsResponse() { Contents = items.Items };
                }
                else
                    throw new ApiEmptyResultException("API response was empty.");
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// player's item inventory. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="steamid64">Player's 64-bit steam id.</param>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>Player's cosmetic items inventory wrapped into ApiResponse object</returns>
        public async Task<PlayerInventoryResponse> GetPlayerItemsAsync(ulong steamid64,
            string apiInterface = IECON_ITEMS, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetPlayerItems", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamid", steamid64.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            PlayerInventoryResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<IReadOnlyDictionary<string, PlayerInventory>>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.TryGetValue("result", out PlayerInventory contents))
                {
                    if (contents.Items == null || contents.Items.Count == 0)
                        throw new ApiEmptyResultException("API response was empty")
                        {
                            ApiStatusCode = contents.Status
                        };
                    else
                        response = new PlayerInventoryResponse() { Contents = contents };
                }
                else
                    throw new ApiEmptyResultException("API response was empty")
                    {
                        ApiStatusCode = contents.Status
                    };
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// item icon path. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="iconName">the item icon name to get the from CDN path.</param>
        /// <param name="iconType">the type of image you want. 0 = normal, 1 = large, 2 = ingame.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>Item icon path wrapped into ApiResponse object.</returns>
        public async Task<ItemIconPathResponse> GetItemIconPathAsync(string iconName, uint iconType = 0,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA_BETA, "GetItemIconPath", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("iconname", iconName)
                .AppendQuery("icontype", iconType.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            ItemIconPathResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaItemIconPathResponseParent>(
                    url, cToken).ConfigureAwait(false);

                if (string.IsNullOrEmpty(webResponse.Result.Contents))
                    throw new ApiEmptyResultException("API response was empty.");
                else
                    response = webResponse.Result;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to api.steampowered.com for item schema url.
        /// </summary>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Schema url string wrapped into ApiResponse object.</returns>
        public async Task<SchemaUrlResponse> GetSchemaUrlAsync(string apiInterface = IECON_ITEMS,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetSchemaURL", version);
            UrlBuilder.AppendQuery("key", ApiKey);

            string url = UrlBuilder.PopEncodedUrl(false);
            SchemaUrlResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaSchemaUrlResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (string.IsNullOrEmpty(webResponse.Result.Contents))
                    throw new ApiEmptyResultException("API response was empty.");
                else
                    response = webResponse.Result;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// store metadata. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="apiInterface">API interface</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellatio token</param>
        /// <returns>StoreMetadata object wrapped into ApiResponse object</returns>
        public async Task<StoreMetadataResponse> GetStoreMetadataAsync(string apiInterface = IECON_ITEMS,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetStoreMetaData", version);
            UrlBuilder.AppendQuery("key", ApiKey);

            string url = UrlBuilder.PopEncodedUrl(false);
            StoreMetadataResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<IReadOnlyDictionary<string, StoreMetadata>>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.TryGetValue("result", out StoreMetadata contents))
                {
                    if (contents == null)
                        throw new ApiEmptyResultException("API response was empty.");
                    else
                        response = new StoreMetadataResponse() { Contents = contents };
                }
                else
                    throw new ApiEmptyResultException("API response was empty.");
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com
        /// for item creator ids for specified item.
        /// Request can be cancelled by providing Cancellation token.
        /// </summary>
        /// <param name="itemDef">item id.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>Item creator id list wrapped into ApiResponse object.</returns>
        public async Task<ItemCreatorsResponse> GetItemCreatorsAsync(uint itemDef,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IECONDOTA, "GetItemCreators", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("itemdef", itemDef.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            ItemCreatorsResponse response = null;
            Exception caughtException = null;
            try
            {
                var webResponse = await GetModelAsync<ItemCreatorsResponse>(url, cToken)
                    .ConfigureAwait(false);
                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// Dota 2 cosmetic item rarities. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="lang">Language.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of Cosmetic rarities wrapped into ApiResponse object.</returns>
        public async Task<CosmeticRaritiesResponse> GetCosmeticRaritiesAsync(CToken cToken = default,
            string apiInterface = IECONDOTA, string version = "v1",  string lang = "en")
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetRarities", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("language", lang);

            string url = UrlBuilder.PopEncodedUrl(false);
            CosmeticRaritiesResponse response = null;
            Exception caughtException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaCosmeticRaritiesResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse.Result;
            }
            catch (Exception thrownException)
            {
                caughtException = thrownException;
            }
            return WrapResponse(response, url, caughtException);
        }

        #endregion

        #region [Get Tournament Data]

        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// players tournament stats. Request can be cancelled by
        /// providing cancellation token.
        /// </summary>
        /// <param name="id32">32-bit steam id.</param>
        /// <param name="heroId">Dota 2 hero id.</param>
        /// <param name="leagueId32">Dota 2 league id.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>TournamentPlayerStats object wrapped into ApiResponse object.</returns>
        public async Task<TournamentPlayerStatsResponse> GetTournamentPlayerStatsAsync(uint id32, uint leagueId32,
            string apiInterface = IDOTA2_MATCH, string version = "v2", uint heroId = 0, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTournamentPlayerStats", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("account_id", id32.ToString())
                .AppendQuery("league_id", leagueId32.ToString())
                .AppendQuery("hero_id", heroId.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            TournamentPlayerStatsResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<TournamentPlayerStatsResponse>(url, cToken)
                    .ConfigureAwait(false);

                uint status = webResponse.Contents != null ? webResponse.Contents.Status : 0;

                if (webResponse.Contents == null)
                    throw new ApiEmptyResultException("API response was empty.");
                else if (status != 0 && status != 1)
                {
                    response = webResponse;
                    response.Status = response.Contents.Status;
                    response.StatusDetail = response.Contents.StatusDetail;
                    response.Contents = null;
                    throw new ApiEmptyResultException(response.StatusDetail);
                }
                else if (webResponse.Contents.Id32 == 0)
                    throw new ApiEmptyResultException("API response was empty."); 
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
        /// Sends GET request to https://api.steampowered.com for
        /// real time match stats. Request can be cancelled by
        /// providing cancellation token.
        /// </summary>
        /// <param name="serverId">Steam server id.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>RealtimeMatchStats wrapped into ApiResponse object.</returns>
        public async Task<RealtimeMatchStatsResponse> GetRealtimeMatchStatsAsync(ulong serverId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_MATCH_STATS, "GetRealtimeStats", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("server_steam_id", serverId.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            RealtimeMatchStatsResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<RealtimeMatchStats>(url, cToken)
                    .ConfigureAwait(false);
                if (webResponse == null || webResponse.Match.MatchId == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = new RealtimeMatchStatsResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// live league games. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="matchId64">Dota 2 match id.</param>
        /// <param name="leagueId32">Dota 2 league id.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of LiveLeagueMatches wrapped into ApiResponse object.</returns>
        public async Task<LiveLeagueMatchesResponse> GetLiveLeagueGamesAsync(string version = "v1",
            ulong matchId64 = 0, uint leagueId32 = 0, CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(IDOTA2_MATCH, "GetLiveLeagueGames", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("match_id", matchId64.ToString())
                .AppendQuery("league_id", leagueId32.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            LiveLeagueMatchesResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<LiveLeagueMatchesResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count  == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse.Result;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// leaguelistings. Request can be cancelled by 
        /// providing cancellation token.
        /// </summary>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of Leagues wrapped into ApiResponse object.</returns>
        public async Task<LeagueListingResponse> GetLeagueListingAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath("IDOTA2Match_205790", "GetLeagueListing", version);
            UrlBuilder.AppendQuery("key", ApiKey);

            string url = UrlBuilder.PopEncodedUrl(false);
            LeagueListingResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<LeagueListingResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse.Result;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// dota 2 tournament prizepool. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="leagueId">Dota 2 league id.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>TournamentPrizePool wrapped into ApiResponse object.</returns>
        public async Task<TournamentPrizePoolResponse> GetTournamentPrizePoolAsync(uint leagueId,
            string apiInterface = IECONDOTA, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTournamentPrizePool", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("leagueid", leagueId.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            TournamentPrizePoolResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<TournamentPrizePoolResponse>(url, cToken)
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


        /// <summary>
        /// Sends GET request to https://www.dota2.com for league
        /// node data. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="leagueId">Dota 2 league id.</param>
        /// <param name="nodeId">Dota 2 League node id.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>LeagueNode wrapped into ApiResponse object.</returns>
        public async Task<LeagueNodeResponse> GetLeagueNodeAsync(uint leagueId, ulong nodeId,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetLeagueNodeData", version);
            UrlBuilder.AppendQuery("league_id", leagueId.ToString())
                .AppendQuery("node_id", nodeId.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            LeagueNodeResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<LeagueNode>(url, cToken)
                    .ConfigureAwait(false);
                if (webResponse == null || webResponse.NodeId == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = new LeagueNodeResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://www.dota2.com for tournament infos
        /// beginning from provided Unix timestamp. If timestamp is not
        /// provided request uses current timestamp. Request may be
        /// narrowed down specifying min tier. Request can be cancelled
        /// providing cancellation token.
        /// </summary>
        /// <param name="timestamp">Query startime as unix timestamp.</param>
        /// <param name="version">API method version.</param>
        /// <param name="minTier">min tier included.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns></returns>
        public async Task<TournamentInfoResponse> GetTournamentInfoAsync(long timestamp = -1,
            string version = "v1", uint minTier = 1, CToken cToken = default)
        {
            try
            {
                CreateUrlForTournamentInfo(version, ValidateTimestamp(timestamp), minTier);
            }
            catch (Exception ex)
            {
                return WrapResponse<TournamentInfoResponse>(null, string.Empty, ex);
            }
            return await GetTournamentInfo(cToken);
        }


        /// <summary>
        /// Sends GET request to https://www.dota2.com for
        /// tournament infos beginnning from provided date.
        /// Request may be narrowed down specifying min tier.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="datetime">Query startime.</param>
        /// <param name="version">API method version.</param>
        /// <param name="minTier">min tier included.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns></returns>
        public async Task<TournamentInfoResponse> GetTournamentInfoAsync(DateTime datetime,
            string version = "v1", uint minTier = 1, CToken cToken = default)
        {
            try
            {
                CreateUrlForTournamentInfo(version, GetUnixTimestampFromDate(datetime), minTier);
            }
            catch (Exception ex)
            {
                return WrapResponse<TournamentInfoResponse>(null, string.Empty, ex);
            }
            return await GetTournamentInfo(cToken);
        }


        /// <summary>
        /// Get tournament info.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>TournamentInfo wrapped into ApiResponse object.</returns>
        private async Task<TournamentInfoResponse> GetTournamentInfo(CToken cToken)
        {
            string url = UrlBuilder.PopEncodedUrl(false);
            TournamentInfoResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<TournamentInfoResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Contents == null || webResponse.Contents.Count == 0)
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


        /// <summary>
        /// Creates url for GetTournamentInfoAsync methods
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
        /// Sends GET request to https://www.dota2.com for 
        /// The International pricepool. Request can be
        /// cancelled by providing cancellation token.
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>TI prize pool wrapped into API response object.</returns>
        public async Task<InternationalPrizePoolResponse> GetInternationalPrizePoolAsync(
            CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("jsfeed", "intlprizepool");

            string url = UrlBuilder.PopEncodedUrl(false);
            InternationalPrizePoolResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<InternationalPrizePoolResponse>(url, cToken)
                    .ConfigureAwait(false);

                if (response.Contents == 0)
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


        /// <summary>
        /// Sends GET request to https://www.dota2.com for recent
        /// and upcoming DCP events. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of recent dcp events wrapped into ApiResponse object.</returns>
        public async Task<RecentDcpEventsResponse> GetRecentDcpEventsAsync(string version = "v1",
            CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetRecentAndUpcomingMatches", version);

            string url = UrlBuilder.PopEncodedUrl(false);
            RecentDcpEventsResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<RecentDcpEvents>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Tournaments == null || webResponse.Tournaments.Count == 0)
                    throw new ApiEmptyResultException("API response was empty.");
                else
                    response = new RecentDcpEventsResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://www.dota2.com for
        /// dota 2 team info. DCP results can be included to request.
        /// Request can be cancelled by provding cancellation token.
        /// </summary>
        /// <param name="teamId">Dota 2 team id.</param>
        /// <param name="includeDCP">Include dcp points.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>Dota 2 Team wrapped into ApiResponse object.</returns>
        public async Task<DotaTeamResponse> GetDotaTeamAsync(ulong teamId, string version = "v1",
            bool includeDCP = false, CToken cToken = default)
        {
            UrlBuilder.Host = DOTA_2_HOST;
            UrlBuilder.AppendPath("webapi", "IDOTA2DPC", "GetSingleTeamInfo", version);
            UrlBuilder.AppendQuery("team_id", teamId.ToString());
            if (includeDCP)
                UrlBuilder.AppendQuery("get_dpc_info", "1");

            string url = UrlBuilder.PopEncodedUrl(false);
            DotaTeamResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaTeam>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.TeamId == 0 || webResponse.Members.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = new DotaTeamResponse() { Contents = webResponse };
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
        }


        /// <summary>
        /// Sends GET request to https://api.steampowered.com for
        /// DotaTeamInfos by providing start team
        /// id. Default request size is 100 teams. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="startId">Team id to start request.</param>
        /// <param name="count">Size of the request.</param>
        /// <param name="apiInterface">API interface.</param>
        /// <param name="version">API method version.</param>
        /// <param name="cToken">Cancellation token.</param>
        /// <returns>List of DotaTeamInfos wrapped into ApiResponse object.</returns>
        public async Task<DotaTeamInfosResponse> GetDotaTeamInfosByIdAsync(ulong startId = 1,
            uint count = 100, string apiInterface = IDOTA2_MATCH,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = STEAM_HOST;
            UrlBuilder.AppendPath(apiInterface, "GetTeamInfoByTeamID", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("start_at_team_id", startId.ToString())
                .AppendQuery("teams_requested", count.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            DotaTeamInfosResponse response = null;
            Exception thrownException = null;
            try
            {
                cToken.ThrowIfCancellationRequested();

                var webResponse = await GetModelAsync<DotaTeamInfosResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (webResponse.Result.Contents == null || webResponse.Result.Contents.Count == 0)
                    throw new ApiEmptyResultException("API response was empty");
                else
                    response = webResponse.Result;
            }
            catch (Exception caughtException)
            {
                thrownException = caughtException;
            }
            return WrapResponse(response, url, thrownException);
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

        //[Obsolete("This method is incomplete")] // TODO: (GetEventStatsForAccountAsync) Figure out response object model
        //public async Task<ushort> GetEventStatsForAccountAsync(uint steamId32, uint eventId,
        //    string version = "v1", CToken cToken = default)
        //{
        //    UrlBuilder.Host = STEAM_HOST;
        //    UrlBuilder.AppendPath(IECONDOTA, "GetEventStatsForAccount", version);
        //    UrlBuilder.AppendQuery("key", ApiKey)
        //        .AppendQuery("accountid", steamId32.ToString())
        //        .AppendQuery("eventid", eventId.ToString());

        //    dynamic response = JObject.Parse(await GetStringAsync(cToken: cToken)
        //        .ConfigureAwait(false));

        //    return response.result.event_points;
        //}

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
