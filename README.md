# steam-api-core
This project provides libraries for calling Valve's
REST APIs targeting netstandard 2.0. It focuses mostly on DOTA 2 API calls, since they are plentiful
## API Methods
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
