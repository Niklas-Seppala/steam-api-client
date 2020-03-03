using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class DotaTeamInfosResponse
    {
        public DotaTeamInfosContent Result { get; set; }
    }

    internal class DotaTeamInfosContent
    {
        public uint Status { get; set; }
        public IReadOnlyList<DotaTeamInfo> Teams { get; set; }
    }
}
