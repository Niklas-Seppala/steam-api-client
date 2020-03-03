using System.Collections.Generic;

namespace SteamApi.Models
{
    public class ApiInterface
    {
        public string Name { get; set; }
        public IReadOnlyList<Method> Methods { get; set; }
    }
}
