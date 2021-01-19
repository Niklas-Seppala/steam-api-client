# steam-api-core
This project provides methods for calling Valve's
REST APIs and staticly typed response classes for all API calls.
All GET methods are asynchronous and can be cancelled with `CancellationToken`.

Methods have built in error checking system, so the user dont have to bother with making sense of weird and inconsistent return codes and values. ( They can be pretty wild sometines :slightly_smiling_face: ). 

Targets netstandard 2.0

Note: this is a hobby project!
### Quick example
```c#
// Setup
ApiClient.SetApiKey("YOUR DEV KEY HERE");
var dotaClient = new DotaApiClient();

var matchesResp = await dotaClient.GetMatchHistoryAsync();
if (matchesResp.Successful)
{
    foreach (var match in matchesResp.Contents)
    {
        Console.WriteLine(match.Id);
    }
}
```

## Supported API methods
### Dota 2
###### IDOTA2Fantasy_205790
<pre>GET GetFantasyPlayerStats/v1         IMPLEMENTED ✔️
GET GetPlayerOfficialInfo/v1         IMPLEMENTED ✔️
GET GetProPlayerList/v1              IMPLEMENTED ✔️</pre>
###### IDOTA2MatchStats_570
<pre>GET GetRealtimeStats/v1              IMPLEMENTED ✔️</pre>
###### IDOTA2Match_205790
<pre>GET GetLeagueListing/v1              IMPLEMENTED ✔️</pre>
###### IDOTA2Match_570
<pre>GET GetLiveLeagueGames/v1            IMPLEMENTED ✔️
GET GetMatchDetails/v1               IMPLEMENTED ✔️
GET GetMatchHistory/v1               IMPLEMENTED ✔️
GET GetMatchHistoryBySequenceNum/v1  IMPLEMENTED ✔️
GET GetTeamInfoByTeamID/v1           IMPLEMENTED ✔️
GET GetTopLiveEventGame/v1           IMPLEMENTED ✔️
GET GetTopLiveGame/v1                IMPLEMENTED ✔️
GET GetTopWeekendTourneyGames/v1     IMPLEMENTED ✔️ 
GET GetTournamentPlayerStats/v2      IMPLEMENTED ✔️</pre>
###### IDOTA2StreamSystem_205790
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️</pre>
###### IDOTA2StreamSystem_570
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️</pre>
###### IDOTA2Ticket_570
<pre>GET ClaimBadgeReward/v1              IMPLEMENTED ❌
GET GetSteamIDForBadgeID/v1          IMPLEMENTED ❌
GET SteamAccountValidForBadgeType/v1 IMPLEMENTED ❌</pre>
###### IEconDOTA2_205790
<pre>GET GetItemIconPath/v1               IMPLEMENTED ✔️</pre>
###### IEconDOTA2_570
<pre>GET GetEventStatsForAccount/v1       IMPLEMENTED ✔️
GET GetGameItems/v1                  IMPLEMENTED ✔️
GET GetHeroes/v1                     IMPLEMENTED ✔️
GET GetItemCreators/v1               IMPLEMENTED ✔️
GET GetRarities/v1                   IMPLEMENTED ✔️
GET GetTournamentPrizePool/v1        IMPLEMENTED ✔️</pre>
###### IEconItems_570
<pre>GET GetEquippedPlayerItems/v1        IMPLEMENTED ✔️
GET GetPlayerItems/v1                IMPLEMENTED ✔️
GET GetSchemaURL/v1                  IMPLEMENTED ✔️
GET GetStoreMetaData/v1              IMPLEMENTED ✔️</pre>

### Steam
###### ISteamWebAPIUtil
<pre>GetServerInfo/v1                     IMPLEMENTED ✔️
GetSupportedAPIList/v1               IMPLEMENTED ✔️</pre>
###### ISteamUser
<pre>GetFriendList/v1                     IMPLEMENTED ✔️
GetPlayerSummaries/v2                IMPLEMENTED ✔️
GetPlayerBans/v1                     IMPLEMENTED ✔️</pre>
###### ISteamUser
<pre>GetNewsForApp/v2                     IMPLEMENTED ✔️</pre>
###### IStoreService
<pre>GetAppList/v1                        IMPLEMENTED ✔️</pre>

### CsGo
###### ICSGOServers_730
<pre>GetGameServersStatus/v1              IMPLEMENTED ✔️</pre>
