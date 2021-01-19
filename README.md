# Valve REST API Clients for .NET
This project provides methods for calling Valve's REST APIs.
Server responses are typically in JSON, and this library converts that date to statically typed C# objects.
Methods for downloading hero/profile pictures are also provided.

All GET methods are asynchronous and can be cancelled with `CancellationToken`.

GET Methods have built in error checking system, so the user doesn't have to bother with making sense of weird and inconsistent return codes and values. ( They can be pretty wild sometines :slightly_smiling_face: ). 

**Targets netstandard 2.0**

Note: this is a hobby project!
## Quick example
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
## Guide

### Setup
* Set the developer key. This has to be done only once.
```c#
ApiClient.SetApiKey("Api key");
```
* Create client object that targets the API.
```c#
var dotaClient = new DotaApiClient();
var steamClient = new SteamApiClient();
var csGoClient = new CsGoApiClient();
```

### Usage
All clients are completely reuseable. Just start firing away.

```c#
var dotaClient = new DotaApiClient();
var steamClient = new SteamApiClient();

// Dota 2 players 32-bit Steam id.
uint playerId = 78123870;

// Get the latest 20 Dota 2 matches from player 78123870,
// where hero with id of 32 was played and skill level was very high.
var matchResponse = await dotaClient.GetMatchHistoryAsync(playerId,
    count: 20,
    heroId: 32,
    skillLevel: DotaSkillLevel.VeryHigh);

foreach (var match in matchResponse.Contents)
{
    // Get details for each match.
    var detailsResponse = await dotaClient.GetMatchDetailsAsync(match.Id);

    // Get all the players from each match.
    foreach (var player in detailsResponse.Contents.Players)
    {
        // Get the Steam account for each player.
        var steamAccount = await steamClient.GetSteamAccountAsync(player.Id64);

        // Download players Steam profile pic.
        var picBytes = await steamClient.GetProfilePicBytesAsync(steamAccount.Contents.AvatarMediumURL);
        await File.WriteAllBytesAsync($"{player.PersonaName}.png", picBytes.Contents);

        // Print the player name and link to Steam page.
        Console.WriteLine($"Player name: {player.PersonaName}");
        Console.WriteLine($"Player Steam profile: {steamAccount.Contents.ProfileURL}");
    }
}
```


### Error handling

All response types inherit from common base class, that contains useful data
about possible cases when request was not succesful. This makes it easier to debug and
handle different types of exeptions.

```c#
if (!response.Successful)
{
    Console.WriteLine($"Request URL was: {response.URL}");
    Exception ex = response.ThrownException;

    if (response.WasCancelled)
    {
        Console.WriteLine("You requested cancellation");
    }
    else if (ex != null)
    {
        Console.WriteLine("An Exception was thrown and caught.");
    }
}
```


## Supported API methods


<details>
<summary>Dota 2</summary>
<br>
IDOTA2Fantasy_205790
<pre>GET GetFantasyPlayerStats/v1         IMPLEMENTED ✔️
GET GetPlayerOfficialInfo/v1         IMPLEMENTED ✔️
GET GetProPlayerList/v1              IMPLEMENTED ✔️</pre>
IDOTA2MatchStats_570
<pre>GET GetRealtimeStats/v1              IMPLEMENTED ✔️</pre>
IDOTA2Match_205790
<pre>GET GetLeagueListing/v1              IMPLEMENTED ✔️</pre>
IDOTA2Match_570
<pre>GET GetLiveLeagueGames/v1            IMPLEMENTED ✔️
GET GetMatchDetails/v1               IMPLEMENTED ✔️
GET GetMatchHistory/v1               IMPLEMENTED ✔️
GET GetMatchHistoryBySequenceNum/v1  IMPLEMENTED ✔️
GET GetTeamInfoByTeamID/v1           IMPLEMENTED ✔️
GET GetTopLiveEventGame/v1           IMPLEMENTED ✔️
GET GetTopLiveGame/v1                IMPLEMENTED ✔️
GET GetTopWeekendTourneyGames/v1     IMPLEMENTED ✔️ 
GET GetTournamentPlayerStats/v2      IMPLEMENTED ✔️</pre>
IDOTA2StreamSystem_205790
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️</pre>
IDOTA2StreamSystem_570
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️</pre>
IDOTA2Ticket_570
<pre>GET ClaimBadgeReward/v1              IMPLEMENTED ❌
GET GetSteamIDForBadgeID/v1          IMPLEMENTED ❌
GET SteamAccountValidForBadgeType/v1 IMPLEMENTED ❌</pre>
IEconDOTA2_205790
<pre>GET GetItemIconPath/v1               IMPLEMENTED ✔️</pre>
IEconDOTA2_570
<pre>GET GetEventStatsForAccount/v1       IMPLEMENTED ✔️
GET GetGameItems/v1                  IMPLEMENTED ✔️
GET GetHeroes/v1                     IMPLEMENTED ✔️
GET GetItemCreators/v1               IMPLEMENTED ✔️
GET GetRarities/v1                   IMPLEMENTED ✔️
GET GetTournamentPrizePool/v1        IMPLEMENTED ✔️</pre>
IEconItems_570
<pre>GET GetEquippedPlayerItems/v1        IMPLEMENTED ✔️
GET GetPlayerItems/v1                IMPLEMENTED ✔️
GET GetSchemaURL/v1                  IMPLEMENTED ✔️
GET GetStoreMetaData/v1              IMPLEMENTED ✔️</pre>
</details>

<details>
<summary>Steam</summary>
<br>
ISteamWebAPIUtil
<pre>GetServerInfo/v1                     IMPLEMENTED ✔️
GetSupportedAPIList/v1               IMPLEMENTED ✔️</pre>
ISteamUser
<pre>GetFriendList/v1                     IMPLEMENTED ✔️
GetPlayerSummaries/v2                IMPLEMENTED ✔️
GetPlayerBans/v1                     IMPLEMENTED ✔️</pre>
ISteamUser
<pre>GetNewsForApp/v2                     IMPLEMENTED ✔️</pre>
IStoreService
<pre>GetAppList/v1                        IMPLEMENTED ✔️</pre>
</details>


<details>
<summary>CsGo</summary>
<br>
ICSGOServers_730
<pre>GetGameServersStatus/v1              IMPLEMENTED ✔️</pre>
</details>
