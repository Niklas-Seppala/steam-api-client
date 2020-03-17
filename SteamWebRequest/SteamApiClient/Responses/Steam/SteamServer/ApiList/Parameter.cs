using System;

namespace SteamApi.Responses.Steam
{
    /// <summary>
    /// Steam API method parameter
    /// </summary>
    [Serializable]
    public sealed class Parameter
    {
        /// <summary>
        /// Parameter name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parameter type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Is parameter optional
        /// </summary>
        public bool Optional { get; set; }

        /// <summary>
        /// Parameter description
        /// </summary>
        public string Description { get; set; }
    }
}
