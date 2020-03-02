using System.Collections.Generic;

namespace SteamApi.Models
{
    public class ApiInterface
    {
        public string Name { get; set; }
        public IReadOnlyCollection<Method> Methods { get; set; }
    }
}
