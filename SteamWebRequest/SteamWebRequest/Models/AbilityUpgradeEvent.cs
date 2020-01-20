namespace SteamWebRequest.Models
{
    public sealed class AbilityUpgradeEvent
    {
        public uint Ability { get; set; }
        public uint Time { get; set; }
        public byte Level { get; set; }
    }
}
