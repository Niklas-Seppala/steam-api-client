using System.Collections.Generic;

namespace SteamApi.Models.Steam
{
    public class AppNewsCollection
    {
        public uint AppId { get; set; }
        public uint TotalCount { get; set; }
        public IReadOnlyList<AppNews> NewsItems { get; set; }
    }
}
