using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class HeroCompanion
    {
        public string UnitName { get; set; }

        public ushort Item_0 { get; set; }
        public ushort Item_1 { get; set; }
        public ushort Item_2 { get; set; }
        public ushort Item_3 { get; set; }
        public ushort Item_4 { get; set; }
        public ushort Item_5 { get; set; }
        public ushort Backpack_0 { get; set; }
        public ushort Backpack_1 { get; set; }
        public ushort Backpack_2 { get; set; }

        [JsonProperty("item_neutral")]
        public ushort NeutralItem { get; set; }
    }
}
