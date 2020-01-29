using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class AppNewsCollection
    {
        public uint AppId { get; set; }
        public uint TotalCount { get; set; }
        public IReadOnlyCollection<AppNews> NewsItems { get; set; }
    }
}
