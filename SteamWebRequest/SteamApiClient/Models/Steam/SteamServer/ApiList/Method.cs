using System.Collections.Generic;

namespace SteamApi.Models
{
    public class Method
    {
        public string Name { get; set; }
        public uint Version { get; set; }
        public string HttpMethod { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string Description { get; set; }
    }
}
