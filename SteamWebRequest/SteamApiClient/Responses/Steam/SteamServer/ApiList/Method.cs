using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    /// <summary>
    /// Steam API method
    /// </summary>
    [Serializable]
    public sealed class Method
    {
        /// <summary>
        /// Name of the method
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Method version
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Http Method
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// List of the available method parameters
        /// </summary>
        public IReadOnlyList<Parameter> Parameters { get; set; }

        /// <summary>
        /// Method description
        /// </summary>
        public string Description { get; set; }
    }
}
