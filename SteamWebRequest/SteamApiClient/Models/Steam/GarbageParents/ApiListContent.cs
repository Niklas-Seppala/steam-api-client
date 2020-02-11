using System.Collections.Generic;

namespace SteamApi.Models
{
    internal class ApiListContent
    {
        public IReadOnlyCollection<ApiInterface> Interfaces { get; set; }
    }
}
