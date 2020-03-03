using System.Collections.Generic;

namespace SteamApi.Models
{
    internal class ApiListContent
    {
        public IReadOnlyList<ApiInterface> Interfaces { get; set; }
    }
}
