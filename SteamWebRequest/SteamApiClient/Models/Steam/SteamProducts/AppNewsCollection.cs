using System.Collections.Generic;

namespace SteamApi.Models
{
    public class AppNewsCollection
    {
        public uint AppId { get; set; }
        public uint TotalCount { get; set; }
        public IReadOnlyList<AppNews> NewsItems { get; set; }
    }
}
