using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    internal class DotaTeamInfosResponse
    {
        public DotaTeamInfosContent Result { get; set; }
    }

    internal class DotaTeamInfosContent
    {
        public byte Status { get; set; }
        public IReadOnlyCollection<DotaTeamInfo> Teams { get; set; }
    }
}
