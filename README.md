# steam-api-core
## What is it?
This project provides libraries for calling Valve's
RESTful APIs using .net core 3.1.

## API Methods
## Dota 2
#### IDOTA2Fantasy_205790
<pre>**GET** GetFantasyPlayerStats/v1         RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
**GET** GetPlayerOfficialInfo/v1         RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
**GET** GetProPlayerList/v1              RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌</pre>
#### IDOTA2MatchStats_205790
<pre>**GET** GetRealtimeStats/v1              RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IDOTA2MatchStats_570
<pre>**GET** GetRealtimeStats/v1              RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IDOTA2Match_205790
<pre>**GET** GetLeagueListing/v1              RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IDOTA2Match_570
<pre>**GET** GetLiveLeagueGames/v1            RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
**GET** GetMatchDetails/v1               RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetMatchHistory/v1               RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️ # game_mode param doesn't work
**GET** GetMatchHistoryBySequenceNum/v1  RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetTeamInfoByTeamID/v1           RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetTopLiveEventGame/v1           RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
**GET** GetTopLiveGame/v1                RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetTopWeekendTourneyGames/v1     RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
**GET** GetTournamentPlayerStats/v1      RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
**GET** GetTournamentPlayerStats/v2      RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️ # match_id, time_frame doesn't work</pre>
#### IDOTA2StreamSystem_205790
<pre>**GET** GetBroadcasterInfo/v1            RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IDOTA2StreamSystem_570
<pre>**GET** GetBroadcasterInfo/v1            RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IDOTA2Ticket_570
<pre>**GET** ClaimBadgeReward/v1              RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
**GET** GetSteamIDForBadgeID/v1          RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
**GET** SteamAccountValidForBadgeType/v1 RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌</pre>
#### IEconDOTA2_205790
<pre>**GET** GetItemIconPath/v1               RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IEconDOTA2_570
<pre>**GET** GetEventStatsForAccount/v1       RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌ # waiting for chance to test</pre>
**GET** GetGameItems/v1                  RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌ # obsolete
**GET** GetHeroes/v1                     RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
**GET** GetItemCreators/v1               RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
**GET** GetRarities/v1                   RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
**GET** GetTournamentPrizePool/v1        RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌</pre>
#### IEconItems_570
<pre>**GET** GetEquippedPlayerItems/v1        RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetPlayerItems/v1                RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetSchemaURL/v1                  RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
**GET** GetStoreMetaData/v1              RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️</pre>
