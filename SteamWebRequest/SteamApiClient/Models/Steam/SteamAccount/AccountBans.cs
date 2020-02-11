namespace SteamApi.Models
{
    public class AccountBans
    {
        public string SteamId { get; set; }
        public bool CommunityBanned { get; set; }
        public bool VACBanned { get; set; }
        public ushort NumberOfVACBans { get; set; }
        public ushort DaysSinceLastBan { get; set; }
        public ushort NumberOfGameBans { get; set; }
        public string EconomyBan { get; set; }
    }
}
