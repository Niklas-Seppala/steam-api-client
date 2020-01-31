using Newtonsoft.Json.Linq;
using SteamApiClient.Models.Dota;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApiClient.Dota
{
    /// <summary>
    /// Extension methods for SteamHttpClient.
    /// These methods target dota 2 APIs.
    /// </summary>
    public static partial class DotaExtensions
    {
        #region [URL Components]

        #region [Base URL]
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

        private const string STEAMPOWERED = "api.steampowered.com";
        #endregion

        #region [Interfaces]
        // Dota 2 fantasy compendium stats interface
        private const string IDOTA2_FANTASY = "IDOTA2Fantasy_205790";

        // Dota 2 live match stats interfaces
        private const string IDOTA2_MATCH_STATS = "IDOTA2MatchStats_205790";
        private const string IDOTA2_MATCH_STATS_570 = "IDOTA2MatchStats_570";

        // Dota 2 Match Details interfaces
        private const string IDOTA2_MATCH = "IDOTA2Match_205790";
        private const string IDOTA2_MATCH_570 = "IDOTA2Match_570";

        // Dota 2 streaming interfaces
        private const string IDOTA2_SS = "IDOTA2StreamSystem_205790";
        private const string IDOTA2_SS_570 = "IDOTA2StreamSystem_570";

        // Dota 2 event ticket interfaces
        private const string IDOTA2_TICKET = "IDOTA2Ticket_205790";
        private const string IDOTA2_TICKET_570 = "IDOTA2Ticket_570";

        // Dota 2 event stats, game items, heroes, iconpaths etc. interfaces
        private const string IECONDOTA2 = "IEconDOTA2_205790";
        private const string IECONDOTA2_570 = "IEconDOTA2_570";

        // Dota 2 cosmetic items
        private const string IECON_ITEMS = "IEconItems_205790";
        private const string IECON_ITEMS_570 = "IEconItems_570";
        #endregion

        #region [Methods]
        private const string GET_FANTASY_PLAYER_STATS = "GetFantasyPlayerStats";
        private const string GET_PLAYER_OFF_INFO = "GetPlayerOfficialInfo";
        private const string GET_PRO_PLAYERS = "GetProPlayerList";
        private const string GET_REALTIME_STATS = "GetRealtimeStats";
        private const string GET_LEAGUE_LISTING = "GetLeagueListing";
        private const string GET_LIVE_LEAGUE_GAMES = "GetLiveLeagueGames";
        private const string GET_MATCH_DETAILS = "GetMatchDetails";
        private const string GET_MATCH_HISTORY = "GetMatchHistory";
        private const string GET_MATCH_HISTORY_SEQ = "GetMatchHistoryBySequenceNum";
        private const string GET_TEAM_INFO_BY_ID = "GetTeamInfoByTeamID";
        private const string GET_TOP_LIVE_EVENT_GAME = "GetTopLiveEventGame";
        private const string GET_TOP_LIVE_GAME = "GetTopLiveGame";
        private const string GET_TOP_WE_TOURNEY_GAMES = "GetTopWeekendTourneyGames";
        private const string GET_TOURNAMENT_P_STATS = "GetTournamentPlayerStats";
        private const string GET_BC_INFO = "GetBroadcasterInfo";
        private const string GET_CLAIM_BADGE = "ClaimBadgeReward";
        private const string GET_ID_FOR_BADGE = "GetSteamIDForBadgeID";
        private const string GET_ACCOUNT_VALID_FOR_BADGE = "SteamAccountValidForBadgeType";
        private const string GET_EVENT_STATS_FOR_ACC = "GetEventStatsForAccount";
        private const string GET_GAME_ITEMS = "GetGameItems";
        private const string GET_HEROES = "GetHeroes";
        private const string GET_ITEM_ICON_PATH = "GetItemIconPath";
        private const string GET_ITEM_CREATORS = "GetItemCreators";
        private const string GET_ITEM_RARITIES = "GetRarities";
        private const string GET_TOURNAMENT_PRIZE = "GetTournamentPrizePool";
        private const string GET_EQUIPED_P_ITEMS = "GetEquippedPlayerItems";
        private const string GET_PLAYER_ITEMS = "GetPlayerItems";
        private const string GET_SCHEMA_URL = "GetSchemaURL";
        private const string GET_STORE_META_DATA = "GetStoreMetaData";
        #endregion

        #region [Versions]
        private const string V1 = "v1";
        private const string V2 = "v2";
        private const string V3 = "v3";
        #endregion

        #endregion

        #region [Get Cosmetic Items]

        /// <summary>
        /// Sends GET request to api.steampowered.com for players equiped
        /// items for specified hero. Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="steamId64">player's 64-bit steam id</param>
        /// <param name="heroClassId">hero's class id</param>
        /// <param name="apiInterface">api interface</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of EquipedItem objects.</returns>
        public static async Task<IReadOnlyCollection<EquipedItem>> GetEquipedPlayerItemsAsync(
            this SteamHttpClient client, ulong steamId64, ushort heroClassId,
            string apiInterface = IECON_ITEMS_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_EQUIPED_P_ITEMS, V1),
                ("steamid", steamId64.ToString()),
                ("class_id", heroClassId.ToString()),
                ("key", client.DevKey)).Url;

            var response = await client
                .RequestAndDeserialize<IReadOnlyDictionary<string, EquipedCosmeticItems>>(url, token)
                .ConfigureAwait(false);

            return response["result"].Items;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for player's
        /// item inventory. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="steamid64">64-bit steam id</param>
        /// <param name="apiInterface">api interface</param>
        /// <param name="token">cancellation token</param>
        /// <returns>PlayerInventory object</returns>
        public static async Task<PlayerInventory> GetPlayerItemsAsync(this SteamHttpClient client,
            ulong steamid64, string apiInterface = IECON_ITEMS_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_PLAYER_ITEMS, V1),
                ("key", client.DevKey),
                ("steamid", steamid64.ToString())).Url;

            var response = await client
                .RequestAndDeserialize<IReadOnlyDictionary<string, PlayerInventory>>(url, token)
                .ConfigureAwait(false);

            return response["result"];
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for
        /// item icon path.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="iconName">icon name</param>
        /// <returns>icon path</returns>
        public static async Task<string> GetItemIconPathAsync(this SteamHttpClient client,
            string iconName)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, IECONDOTA2, GET_ITEM_ICON_PATH, V1),
                ("key", client.DevKey),
                ("iconname", iconName)).Url;

            dynamic response = JObject.Parse(await SteamHttpClient.Client.GetStringAsync(url)
                .ConfigureAwait(false));
            return response.result.path;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for item schema url.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiInterface">api interface</param>
        /// <returns>item schema url</returns>
        public static async Task<string> GetSchemaUrlAsync(this SteamHttpClient client,
            string apiInterface = IECON_ITEMS_570)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_SCHEMA_URL, V1),
                ("key", client.DevKey)).Url;

            dynamic response = JObject.Parse(await SteamHttpClient.Client.GetStringAsync(url)
                .ConfigureAwait(false));
            return response.result.items_game_url;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for strore metadata.
        /// Default api interface is IEconItems_570.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiInterface">api interface</param>
        /// <param name="token"></param>
        /// <returns>StoreMetadata object.</returns>
        public static async Task<StoreMetadata> GetStoreMetadataAsync(this SteamHttpClient client,
            string apiInterface = IECON_ITEMS_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_STORE_META_DATA, V1),
                ("key", client.DevKey)).Url;

            var response = await client
                .RequestAndDeserialize<IReadOnlyDictionary<string, StoreMetadata>>(url, token)
                .ConfigureAwait(false);
            return response["result"];
        }

        /// <summary>
        /// Sends GET request for item creator ids for
        /// specified item. Request can be cancelled
        /// by providing Cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="itemDef">item id</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyColletion of contributor ids</returns>
        public static async Task<IReadOnlyCollection<uint>> GetItemCreatorsAsync(
            this SteamHttpClient client, string itemDef, CToken token = default)
        {
            var uBuilder = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, IECONDOTA2_570, GET_ITEM_CREATORS, V1),
                ("key", client.DevKey),
                ("itemdef", itemDef));

            var creators = await client.RequestAndDeserialize<ItemCreators>(uBuilder.Url, token)
                .ConfigureAwait(false);

            return creators.Contributors;
        }

        /// <summary>
        /// Sends GET request for Dota 2 cosmetic item rarities.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <param name="lang">language</param>
        /// <returns>ReadOnlyCollection of DotaCosmeticRarities</returns>
        public static async Task<IReadOnlyCollection<DotaCosmeticRarity>> GetDotaCosmeticRaritiesAsync(
            this SteamHttpClient client, CToken token = default, string apiInterface = IECONDOTA2_570, string lang = "en")
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_ITEM_RARITIES, V1),
                ("key", client.DevKey),
                ("language", lang)).Url;

            var result = await client
                .RequestAndDeserialize<DotaCosmeticRaritiesResult>(url, token)
                .ConfigureAwait(false);

            return result.Result.Rarities;
        }

        #endregion

        #region [Leaderboards]

        /// <summary>
        /// Sends GET request for dota 2 leaderboards. Specify
        /// region. Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="region">leaderboard region</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Leaderboard object</returns>
        /// <exception cref="APIException">Thrown when api call fails</exception>
        public static async Task<Leaderboard> GetLeaderboardAsync(this SteamHttpClient client,
            DotaRegion region = default, CToken token = default)
        {
            UrlBuilder uBuilder = new UrlBuilder(URL_LEADERBOARDS);

            switch (region)
            {
                case DotaRegion.Europe:
                    uBuilder.AddQuery(("division", "europe"));
                    break;
                case DotaRegion.America:
                    uBuilder.AddQuery(("division", "americas"));
                    break;
                case DotaRegion.SEA:
                    uBuilder.AddQuery(("division", "se_asia"));
                    break;
                case DotaRegion.China:
                    uBuilder.AddQuery(("division", "china"));
                    break;
            }
            return await client.RequestAndDeserialize<Leaderboard>(uBuilder.Url, token)
                .ConfigureAwait(false);
        }


        #endregion

        #region [Get Abilities]

        /// <summary>
        /// Sends GET request for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyDictionary of abilities</returns>
        public static async Task<IReadOnlyDictionary<string, Ability>> GetAbilitiesDictAsync(this SteamHttpClient client,
            CToken token = default)
        {
            var abilities = await client.RequestAndDeserialize<Abilities>(URL_ABILITY_DATA, token)
                .ConfigureAwait(false);
            return abilities.AbilityDict;
        }

        #endregion

        #region [Get Heroes]

        /// <summary>
        /// Sends GET request for dota heroinfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of heroinfos</returns>
        public static async Task<IReadOnlyDictionary<string, HeroInfo>> GetHeroInfosAsync(
            this SteamHttpClient client, CToken token = default)
        {
            return await client.RequestAndDeserialize<IReadOnlyDictionary<string, HeroInfo>>(URL_HEROINFOS, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 hero stats. Requst
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyDictionary of hero stats</returns>
        public static async Task<IReadOnlyDictionary<string, HeroStats>> GetHeroStatsAsync(
            this SteamHttpClient client, CToken token = default)
        {
            string url = new UrlBuilder(URL_HEROPEDIA_DATA, ("feeds", "herodata")).Url;

            var stats = await client.RequestAndDeserialize<HeroStatsContainer>(url, token)
                .ConfigureAwait(false);

            return stats.HeroStats;
        }

        /// <summary>
        /// Sends GET request for dota 2 heroes. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of Hero objects</returns>
        public static async Task<IReadOnlyCollection<Hero>> GetHeroesAsync(this SteamHttpClient client,
            CToken token = default, string apiInterface = IECONDOTA2_570)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_HEROES, V1),
                ("key", client.DevKey)).Url;

            var result = await client.RequestAndDeserialize<HeroesResponse>(url, token)
                .ConfigureAwait(false);

            return result.Content.Heroes;
        }

        #endregion

        #region [Get Items]


        /// <summary>
        /// Sends GET request to dota2.com for Dota 2 iteminfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyDictionary of item infos</returns>
        public static async Task<IReadOnlyDictionary<string, Item>> GetItemInfoDictAsync(
            this SteamHttpClient client, CToken token = default)
        {
            var items = await client.RequestAndDeserialize<ItemDictionary>(URL_ITEMINFOS, token)
                .ConfigureAwait(false);

            return items.ItemDict;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for dota 2 items. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="token">cancellation token.</param>
        /// <returns>ReadOnlyCollection of Item objects</returns>
        [Obsolete("Use GetItemInfoAsync()")]
        public static async Task<IReadOnlyCollection<Item>> GetGameItemsAsync(this SteamHttpClient client,
            string apiInterface = IECONDOTA2_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_GAME_ITEMS, V1),
                ("key", client.DevKey)).Url;

            var result = await client.RequestAndDeserialize<GameItems>(url, token)
                .ConfigureAwait(false);

            return result.Content.Items;
        }

        #endregion

        #region [Get Unique Users]

        /// <summary>
        /// Sends GET request for unique user count. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of unique users</returns>
        public static async Task<IReadOnlyDictionary<string, uint>> GetUniqueUsersAsync(
            this SteamHttpClient client, CToken token = default)
        {
            return await client.RequestAndDeserialize<Dictionary<string, uint>>(URL_UNIQUE_USERS, token)
                .ConfigureAwait(false);
        }

        #endregion

        #region [Get Match History]

        /// <summary>
        /// Sends GET request to api.steampowered.com for match history
        /// by sequence number. Default interface is IDOTA2Match_570.
        /// Returns list of ! MatchDetails ! unlike other match history methods.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="apiInterface">API interface</param>
        /// <param name="seqNum">the match sequence number to start returning results from</param>
        /// <param name="count">match count</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of MatchDetails objects.</returns>
        public static async Task<IReadOnlyCollection<MatchDetails>> GetMatchHistoryBySequenceNumAsync(
            this SteamHttpClient client, ulong seqNum, string apiInterface = IDOTA2_MATCH_570,
            byte count = 50, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_MATCH_HISTORY_SEQ, V1),
                ("key", client.DevKey),
                ("start_at_match_seq_num", seqNum.ToString()),
                ("matches_requested", count.ToString())).Url;

            var response = await client.RequestAndDeserialize<MatchHistoryBySeqResponse>(url, token)
                .ConfigureAwait(false);

            return response.Result.Matches;
        }

        /// <summary>
        /// Sends a GET request to api.steampowered.com for Dota 2 match history.
        /// Request can be specified by providing optional paramters.
        /// Request can be cancelled by providing CancellationToken.
        /// </summary>
        /// <param name="playerId32">Player's steam id in 32-bit</param>
        /// <param name="count">Match quantity (1-100)</param>
        /// <param name="cancelToken">Cancellation token for API request</param>
        /// <param name="heroId">Limits query to matches with this hero played</param>
        /// <param name="leagueId">Limits query to this league</param>
        /// <param name="minPlayers">minimum number of human players that must be in a match for it to be returned</param>
        /// <param name="skillLevel"> the average skill range of the match, these can be [1-3] with lower numbers being lower skill.
        /// Ignored if an account ID is specified</param>
        /// <param name="startAtId">the minimum match ID to start from</param>
        /// <param name="token">cancellation token</param>
        /// <param name="apiInterface">api interface</param>
        /// <returns>MatchHistoryResponse object</returns>
        public static async Task<MatchHistoryResponse> GetMatchHistoryAsync(this SteamHttpClient client,
            uint playerId32 = 0, uint heroId = 0, byte minPlayers = 0, uint leagueId = 0, ulong startAtId = 0,
            byte count = 25, string apiInterface = IDOTA2_MATCH_570, byte skillLevel = 0, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_MATCH_HISTORY, V1),
                ("key", client.DevKey),
                ("account_id", playerId32.ToString()),
                ("matches_requested", count.ToString()),
                ("hero_id", heroId.ToString()),
                ("min_players", minPlayers.ToString()),
                ("league_id", leagueId.ToString()),
                ("start_at_match_id", startAtId.ToString()),
                ("skill", skillLevel.ToString())).Url;

            var response = await client
                .RequestAndDeserialize<MatchHistoryContainer>(url, token)
                .ConfigureAwait(false);

            return response.History;
        }


        #endregion

        #region [Get Match Details]

        /// <summary>
        /// Sends GET request for Dota 2 match details. Request can be
        /// cancelled by providing cancellation token.
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <param name="token">Cancellation token for API request</param>
        /// <returns>MatchDetails object</returns>
        public static async Task<MatchDetails> GetMatchDetailsAsync(this SteamHttpClient client,
            string matchId, string apiInterface = IDOTA2_MATCH_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_MATCH_DETAILS, V1),
                ("match_id", matchId),
                ("key", client.DevKey),
                ("include_persona_names", "1")).Url;

            var response = await client.RequestAndDeserialize<MatchDetailsContainer>(url, token)
                .ConfigureAwait(false);

            return response.Details;
        }

        #endregion

        #region [Get Live Games]

        // TODO : Check return JSON object when live games are available.
        public static async Task<IReadOnlyCollection<LiveMatch>> GetTopLiveEventGamesAsync(
            this SteamHttpClient client, string apiInterface = IDOTA2_MATCH_570,
            int partner = 1, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_TOP_LIVE_EVENT_GAME, V1),
                ("key", client.DevKey),
                ("partner", partner.ToString())).Url;

            var response = await client.RequestAndDeserialize<TopLiveGames>(url, token)
                .ConfigureAwait(false);

            throw new NotImplementedException("Need to wait for live games to check response JSON object.");
            //return response.Games;
        }

        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <param name="partner">partner id</param>
        /// <returns>ReadOnlyCollection of LiveMatch objects</returns>
        public static async Task<IReadOnlyCollection<LiveMatch>> GetTopLiveGamesAsync(this SteamHttpClient client,
            string apiInterface = IDOTA2_MATCH_570, int partner = 1, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_TOP_LIVE_GAME, V1),
                ("key", client.DevKey),
                ("partner", partner.ToString())).Url;

            var response = await client.RequestAndDeserialize<TopLiveGames>(url, token)
                .ConfigureAwait(false);

            return response.Games;
        }

        #endregion

        #region [Get Tournament Data]

        /// <summary>
        /// Sends GET request to api.steampowered.com for players tournament
        /// stats. Default interface is IDOTA22Match_570 and method version
        /// is v2. Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="accountId32">player 32-bit account id</param>
        /// <param name="leagueId32">league 32-bit id</param>
        /// <param name="heroId">hero id</param>
        /// <param name="apiInterface">api interface</param>
        /// <param name="methodVersion">Method version</param>
        /// <param name="token">cancellation token</param>
        /// <returns>TournamentPlayerStats object.</returns>
        public static async Task<TournamentPlayerStats> GetTournamentPlayerStatsAsync(this SteamHttpClient client,
            uint accountId32, uint leagueId32, string apiInterface = IDOTA2_MATCH_570, string methodVersion = V2,
            ushort heroId = 0, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_TOURNAMENT_P_STATS, methodVersion),
                ("key", client.DevKey),
                ("account_id", accountId32.ToString()),
                ("league_id", leagueId32.ToString()),
                ("hero_id", heroId.ToString())).Url;

            var response = await client.RequestAndDeserialize<TournamentPlayerStatsResponse>(url, token)
                .ConfigureAwait(false);
            return response.Result;

        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for real time match stats.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="serverId">game server id</param>
        /// <param name="token">cancellation token</param>
        /// <param name="apiInterface">api interface</param>
        /// <returns>RealtimeMatchStats object</returns>
        public static async Task<RealtimeMatchStats> GetRealtimeMatchStatsAsync(
            this SteamHttpClient client, string serverId, string apiInterface = IDOTA2_MATCH_STATS_570,
            CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_REALTIME_STATS, V1),
                ("server_steam_id", serverId),
                ("key", client.DevKey)).Url;

            return await client.RequestAndDeserialize<RealtimeMatchStats>(url, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for live
        /// league games. Request can be specifiend by providing
        /// 64-bit match id and 32-bit league id. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="leagueId">32-bit league id</param>
        /// <param name="matchId">64-bit match id</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of LiveLeagueMatch objects.</returns>
        public static async Task<IReadOnlyCollection<LiveLeagueMatch>> GetLiveLeagueMatchAsync(this SteamHttpClient client,
            ulong matchId = 0, uint leagueId = 0, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, IDOTA2_MATCH_570, GET_LIVE_LEAGUE_GAMES, V1),
                ("key", client.DevKey),
                ("match_id", matchId.ToString()),
                ("league_id", leagueId.ToString())).Url;

            var response = await client.RequestAndDeserialize<LiveLeagueMatchResponse>(url, token)
                .ConfigureAwait(false);
            return response.Result.Games;
        }


        // WAITING FOR TESTING
        public static async Task<ushort> GetEventStatsForAccountAsync(this SteamHttpClient client,
            uint steamId32, uint eventId)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, IECONDOTA2_570, GET_EVENT_STATS_FOR_ACC, V1),
                ("key", client.DevKey),
                ("accountid", steamId32.ToString()),
                ("eventid", eventId.ToString())).Url;

            // TODO: Figure what json response looks like.
            dynamic response = JObject.Parse(await SteamHttpClient.Client.GetStringAsync(url).ConfigureAwait(false));
            return response.result.event_points;
        }

        /// <summary>
        /// Sends GET request to api.steampowered.com for leaguelistings.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of League objects</returns>
        public static async Task<IReadOnlyCollection<League>> GetLeagueListingAsync(this SteamHttpClient client,
            CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, IDOTA2_MATCH, GET_LEAGUE_LISTING, V1),
                ("key", client.DevKey)).ToString();

            var response = await client.RequestAndDeserialize<LeagueListingResponse>(url, token)
                .ConfigureAwait(false);

            return response.Result.Leagues;
        }

        /// <summary>
        /// Sends GET request for dota 2 tournament prizepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="leagueId">league/tournament id</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyDictionary of currency and value pairs</returns>
        public static async Task<IReadOnlyDictionary<string, uint>> GetTournamentPrizePoolAsync(
            this SteamHttpClient client, string leagueId, string apiInterface = IECONDOTA2_570,
            CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_TOURNAMENT_PRIZE, V1),
                ("key", client.DevKey),
                ("leagueid", leagueId)).Url;

            var result = await client
                .RequestAndDeserialize<IReadOnlyDictionary<string, IReadOnlyDictionary<string, uint>>>(url, token)
                .ConfigureAwait(false);

            return result["result"];
        }

        /// <summary>
        /// Sends GET request for league node data.
        /// Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="leagueId">league/tournament id</param>
        /// <param name="nodeId">node id</param>
        /// <param name="token">cancellation token</param>
        /// <returns>LeagueNode object.</returns>
        public static async Task<LeagueNode> GetLeagueNodeAsync(this SteamHttpClient client,
             ulong leagueId, ulong nodeId, CToken token = default)
        {
            UrlBuilder uBuilder = new UrlBuilder(URL_IDOTA2DPC + "GetLeagueNodeData/v001/", // TODO: fix this.
                ("league_id", leagueId.ToString()),
                ("node_id", nodeId.ToString()));

            return await client.RequestAndDeserialize<LeagueNode>(uBuilder.Url, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for tournament infos beginning from provided
        /// Unix Timestamp. If Unix Timestamp is not provided request uses
        /// current timestamp. Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="timestamp">Unix Timestamp of tournament start times</param>
        /// <param name="maxTier">max tier included in request</param>
        /// <param name="minTier">min tier inclided in request</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of TournamentInfo objects</returns>
        public static async Task<IReadOnlyCollection<TournamentInfo>> GetTournamentInfoAsync(
            this SteamHttpClient client, long timestamp = -1, byte maxTier = 5, byte minTier = 1, CToken token = default)
        {
            UrlBuilder uBuilder = CreateUrlForTournamentInfo(client.GetTimestamp(timestamp), maxTier, minTier);

            var result = await client
                .RequestAndDeserialize<TournamentInfoCollection>(uBuilder.Url, token)
                .ConfigureAwait(false);

            return result.Infos;
        }

        /// <summary>
        /// Sends GET request for tournament infos beginnning
        /// from provided date.Request may be narrowed down specifying max
        /// and min tiers. Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="datetime">DateTime of tournament start times</param>
        /// <param name="maxTier">max tier included in request</param>
        /// <param name="minTier">min tier inclided in request</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of TournamentInfo objects</returns>
        public static async Task<IReadOnlyCollection<TournamentInfo>> GetTournamentInfoAsync(
            this SteamHttpClient client, DateTime datetime, byte maxTier = 5, byte minTier = 1, CToken token = default)
        {
            string url = CreateUrlForTournamentInfo(client.GetTimestamp(datetime), maxTier, minTier).Url;

            var result = await client.RequestAndDeserialize<TournamentInfoCollection>(url, token)
                .ConfigureAwait(false);

            return result.Infos;
        }

        /// <summary>
        /// Sends GET request for Internation pricepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of currency and money sum</returns>
        public static async Task<IReadOnlyDictionary<string, uint>> GetInternationalPrizePoolAsync(
            this SteamHttpClient client, CToken token = default)
        {
            return await client
                .RequestAndDeserialize<IReadOnlyDictionary<string, uint>>(URL_INT_PRIZEPOOL, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for recent and upcoming DCP
        /// events. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>RecentDcpEvents object.</returns>
        public static async Task<RecentDcpEvents> GetRecentDcpEventsAsync(
            this SteamHttpClient client, CToken token = default)
        {
            string url = URL_IDOTA2DPC + "/GetRecentAndUpcomingMatches/v1";

            return await client.RequestAndDeserialize<RecentDcpEvents>(url, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 team info. DCP results
        /// can be included to request. Request can be cancelled by provding
        /// cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="teamId">dota 2 team id</param>
        /// <param name="includeDCP">include dcp results</param>
        /// <param name="token">cancellation token</param>
        /// <returns>DotaTeam object.</returns>
        public static async Task<DotaTeam> GetDotaTeamAsync(
            this SteamHttpClient client, string teamId, bool includeDCP = false, CToken token = default)
        {
            UrlBuilder uBuilder = new UrlBuilder(URL_IDOTA2DPC + "/GetSingleTeamInfo/v001/",
                ("team_id", teamId));

            if (includeDCP)
            {
                uBuilder.AddQuery(("get_dpc_info", "1"));
            }
            return await client.RequestAndDeserialize<DotaTeam>(uBuilder.Url, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for DotaTeamInfos by providing start team
        /// id. Default request size is 100 teams. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="startId">starting team id</param>
        /// <param name="count">request size</param>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of DotaTeamInfo objects</returns>
        public static async Task<IReadOnlyCollection<DotaTeamInfo>> GetDotaTeamInfosByIdAsync(
            this SteamHttpClient client, ulong startId = 1, uint count = 100,
            string apiInterface = IDOTA2_MATCH_570, CToken token = default)
        {
            string url = new UrlBuilder(
                UrlBuilder.CreateBaseApiUrl(STEAMPOWERED, apiInterface, GET_TEAM_INFO_BY_ID, V1),
                ("key", client.DevKey),
                ("start_at_team_id", startId.ToString()),
                ("teams_requested", count.ToString())).Url;

            var response = await client.RequestAndDeserialize<DotaTeamInfosResponse>(url, token)
                .ConfigureAwait(false);

            return response.Result.Teams;
        }

        #endregion

        #region [Get Player Profile]

        /// <summary>
        /// Sends GET request for dota 2 player profile.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="id32">player 32-bit steam id</param>
        /// <param name="token">cancellation token</param>
        /// <returns>DotaPlayerProfile object</returns>
        public static async Task<DotaPlayerProfile> GetDotaPlayerProfileAsync(this SteamHttpClient client,
            string id32, CToken token = default)
        {
            var uBuilder = new UrlBuilder(URL_IDOTA2DPC + "/GetPlayerInfo/v001/",
                ("account_id", id32));

            return await client
                .RequestAndDeserialize<DotaPlayerProfile>(uBuilder.Url, token)
                .ConfigureAwait(false);
        }

        #endregion

        #region [Get Images]

        /// <summary>
        /// Sends GET request for hero image.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="heroName">hero name</param>
        /// <param name="imgShape">shape of the image</param>
        /// <returns>hero image</returns>
        public static async Task<Image> GetHeroImageAsync(this SteamHttpClient client,
            string heroName, ImageShape imgShape = ImageShape.Horizontal)
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
            return await client
                .GetImageAsync(sBuilder.ToString())
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for item image.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="imgName">image name</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>item image</returns>
        public static async Task<Image> GetItemImageAsync(this SteamHttpClient client,
            string imgName, CToken token = default)
        {
            StringBuilder sBuilder = new StringBuilder(URL_ITEM_IMG);
            sBuilder.Append(imgName);

            return await client
                .GetImageAsync(sBuilder.ToString(), token)
                .ConfigureAwait(false);
        }

        #endregion

        #region [Utility]

        /// <summary>
        /// Creates url for GetTournamentInfoAsync() methods
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <param name="maxTier">max tier included</param>
        /// <param name="minTier">min tier included</param>
        /// <returns>UrlBuilder object that contains the url.</returns>
        /// <exception cref="APIException"></exception>
        private static UrlBuilder CreateUrlForTournamentInfo(ulong timestamp, byte maxTier, byte minTier)
        {
            UrlBuilder uBuilder = new UrlBuilder(URL_IDOTA2DPC + "GetLeagueInfoList/v001");
            if (minTier > maxTier)
            {
                throw new APIException("Minimum tier can't be larger than maximum tier")
                { URL = uBuilder.Url };
            }
            uBuilder.AddQuery(("start_timestamp", timestamp.ToString()));
            uBuilder.AddQuery(("min_tier", minTier.ToString()));
            uBuilder.AddQuery(("max_tier", maxTier.ToString()));
            return uBuilder;
        }

        #endregion
    }
}
