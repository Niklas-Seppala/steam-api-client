# steam-api-core
## What is it?
This project provides libraries for calling Valve's
RESTful APIs using .net core 3.1.

## API Methods
## Dota 2
#### IDOTA2Fantasy_205790
GET IDOTA2Fantasy_205790/GetFantasyPlayerStats/v1               RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
GET IDOTA2Fantasy_205790/GetPlayerOfficialInfo/v1               RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
GET IDOTA2Fantasy_205790/GetProPlayerList/v1                    RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
#### IDOTA2MatchStats_205790
GET IDOTA2MatchStats_205790/GetRealtimeStats/v1                 RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
#### IDOTA2MatchStats_570
GET IDOTA2MatchStats_570/GetRealtimeStats/v1                    RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
#### IDOTA2Match_205790
GET IDOTA2Match_205790/GetLeagueListing/v1                      RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
#### IDOTA2Match_570
GET IDOTA2Match_570/GetLiveLeagueGames/v1                       RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
GET IDOTA2Match_570/GetMatchDetails/v1                          RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IDOTA2Match_570/GetMatchHistory/v1                          RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️ # game_mode param doesn't work
GET IDOTA2Match_570/GetMatchHistoryBySequenceNum/v1             RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IDOTA2Match_570/GetTeamInfoByTeamID/v1                      RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IDOTA2Match_570/GetTopLiveEventGame/v1                      RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌ # waiting for chance to test
GET IDOTA2Match_570/GetTopLiveGame/v1                           RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IDOTA2Match_570/GetTopWeekendTourneyGames/v1                RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
GET IDOTA2Match_570/GetTournamentPlayerStats/v1                 RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
GET IDOTA2Match_570/GetTournamentPlayerStats/v2                 RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️ # match_id, time_frame doesn't work
#### IDOTA2StreamSystem_205790
GET IDOTA2StreamSystem_205790/GetBroadcasterInfo/v1             RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
#### IDOTA2StreamSystem_570
GET IDOTA2StreamSystem_570/GetBroadcasterInfo/v1                RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
#### IDOTA2Ticket_570
GET IDOTA2Ticket_570/ClaimBadgeReward/v1                        RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
GET IDOTA2Ticket_570/GetSteamIDForBadgeID/v1                    RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
GET IDOTA2Ticket_570/SteamAccountValidForBadgeType/v1           RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌
#### IEconDOTA2_205790
GET IEconDOTA2_205790/GetItemIconPath/v1                        RECOMMENDED ❌ IMPLEMENTED ✔️ TESTED ❌
#### IEconDOTA2_570
GET IEconDOTA2_570/GetEventStatsForAccount/v1                   RECOMMENDED ✔️ IMPLEMENTED ❌ TESTED ❌ # waiting for chance to test
GET IEconDOTA2_570/GetGameItems/v1                              RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌ # obsolete
GET IEconDOTA2_570/GetHeroes/v1                                 RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
GET IEconDOTA2_570/GetItemCreators/v1                           RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
GET IEconDOTA2_570/GetRarities/v1                               RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
GET IEconDOTA2_570/GetTournamentPrizePool/v1                    RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ❌
#### IEconItems_570
GET IEconItems_570/GetEquippedPlayerItems/v1                    RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IEconItems_570/GetPlayerItems/v1                            RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IEconItems_570/GetSchemaURL/v1                              RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
GET IEconItems_570/GetStoreMetaData/v1                          RECOMMENDED ✔️ IMPLEMENTED ✔️ TESTED ✔️
