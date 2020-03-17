using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Realtime dota 2 match building state model
    /// </summary>
    [Serializable]
    public sealed class RealTimeMatchBuilding
    {
        /// <summary>
        /// Whose building
        /// </summary>
        public uint Team { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Heading { get; set; }

        /// <summary>
        /// Building type
        /// </summary>
        public uint Type { get; set; }

        /// <summary>
        /// Where is the building
        /// </summary>
        public uint Lane { get; set; }

        /// <summary>
        /// X-coordinate on minimap
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// y-coordinate on minimap
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Destroyed flag
        /// </summary>
        public bool Destroyed { get; set; }
    }
}
