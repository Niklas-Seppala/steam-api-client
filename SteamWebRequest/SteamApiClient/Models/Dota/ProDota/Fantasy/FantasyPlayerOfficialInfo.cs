namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Fantasy player info model
    /// </summary>
    public class FantasyPlayerOfficialInfo
    {
        /// <summary>
        /// Player's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// PLayer's team name
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Player's team tag
        /// </summary>
        public string TeamTag { get; set; }

        /// <summary>
        /// Player's team's sponsor
        /// </summary>
        public string Sponsor { get; set; }

        /// <summary>
        /// Id of the player's fantasy role
        /// </summary>
        public uint FantasyRole { get; set; }
    }
}
