namespace SteamApi
{
    /// <summary>
    /// Dota 2 game modes according to old
    /// dota2 API documentation. (Not to be completely trusted).
    /// </summary>
    public enum GameMode
    {
        None = 0,
        AllPick = 1,
        CaptainsMode = 2,
        RandomDraft = 3,
        SingleDraft = 4,
        AllRandom = 5,
        Intro = 6,
        Diretide = 7,
        ReverseCaptainsMode = 8,
        TheGreeviling = 9,
        Tutorial = 10,
        MidOnly = 11,
        LeastPlayed = 12,
        NewPlayerPool = 13,
        CompediumMatchmaking = 14,
        CoopVsBots = 15,
        CaptainsDraft = 16,
        AbilityDraft = 18,
        AllRandomDeathMatch = 20,
        One_V_OneMidOnly = 21,
        RankedMatchMaking = 23
    }
}
