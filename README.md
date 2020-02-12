# steam-api-core
## What is it?
This project provides libraries for calling Valve's
RESTful APIs targeting netstandard 2.0.
## API Methods
### Dota 2
###### IDOTA2Fantasy_205790
<pre>GET GetFantasyPlayerStats/v1         IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET GetPlayerOfficialInfo/v1         IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET GetProPlayerList/v1              IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test</pre>
###### IDOTA2MatchStats_570
<pre>GET GetRealtimeStats/v1              IMPLEMENTED ✔️ TESTED ✔️</pre>
###### IDOTA2Match_205790
<pre>GET GetLeagueListing/v1              IMPLEMENTED ✔️ TESTED ✔️</pre>
###### IDOTA2Match_570
<pre>GET GetLiveLeagueGames/v1            IMPLEMENTED ✔️ TESTED ✔️
GET GetMatchDetails/v1               IMPLEMENTED ✔️ TESTED ✔️
GET GetMatchHistory/v1               IMPLEMENTED ✔️ TESTED ✔️ # game_mode
GET GetMatchHistoryBySequenceNum/v1  IMPLEMENTED ✔️ TESTED ✔️
GET GetTeamInfoByTeamID/v1           IMPLEMENTED ✔️ TESTED ✔️
GET GetTopLiveEventGame/v1           IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET GetTopLiveGame/v1                IMPLEMENTED ✔️ TESTED ✔️
GET GetTopWeekendTourneyGames/v1     IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET GetTournamentPlayerStats/v1      IMPLEMENTED ✔️ TESTED ❌
GET GetTournamentPlayerStats/v2      IMPLEMENTED ✔️ TESTED ✔️ # match_id, time_frame</pre>
###### IDOTA2StreamSystem_205790
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️ TESTED ❌</pre>
###### IDOTA2StreamSystem_570
<pre>GET GetBroadcasterInfo/v1            IMPLEMENTED ✔️ TESTED ❌</pre>
###### IDOTA2Ticket_570
<pre>GET ClaimBadgeReward/v1              IMPLEMENTED ✔️ TESTED ❌ # Returns JSON-string (no idea what this does)
GET GetSteamIDForBadgeID/v1          IMPLEMENTED ✔️ TESTED ❌ # Returns JSON-string (no idea what this does)
GET SteamAccountValidForBadgeType/v1 IMPLEMENTED ✔️ TESTED ❌ # Returns JSON-string (no idea what this does)</pre>
###### IEconDOTA2_205790
<pre>GET GetItemIconPath/v1               IMPLEMENTED ✔️ TESTED ✔️</pre>
###### IEconDOTA2_570
<pre>GET GetEventStatsForAccount/v1       IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET GetGameItems/v1                  IMPLEMENTED ✔️ TESTED ✔️
GET GetHeroes/v1                     IMPLEMENTED ✔️ TESTED ✔️
GET GetItemCreators/v1               IMPLEMENTED ✔️ TESTED ✔️
GET GetRarities/v1                   IMPLEMENTED ✔️ TESTED ✔️
GET GetTournamentPrizePool/v1        IMPLEMENTED ✔️ TESTED ✔️</pre>
###### IEconItems_570
<pre>GET GetEquippedPlayerItems/v1        IMPLEMENTED ✔️ TESTED ✔️
GET GetPlayerItems/v1                IMPLEMENTED ✔️ TESTED ✔️
GET GetSchemaURL/v1                  IMPLEMENTED ✔️ TESTED ✔️
GET GetStoreMetaData/v1              IMPLEMENTED ✔️ TESTED ✔️</pre>
