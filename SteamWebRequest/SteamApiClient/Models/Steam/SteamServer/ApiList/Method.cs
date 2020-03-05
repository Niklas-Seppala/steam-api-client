using System.Collections.Generic;

namespace SteamApi.Models.Steam
{
    public class Method
    {
        public string Name { get; set; }
        public uint Version { get; set; }
        public string HttpMethod { get; set; }
        public IReadOnlyList<Parameter> Parameters { get; set; }
        public string Description { get; set; }
    }
}
