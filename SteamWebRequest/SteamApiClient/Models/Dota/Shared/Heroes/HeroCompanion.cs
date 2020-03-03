using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class HeroCompanion
    {
        public string UnitName { get; set; }
        public uint Item_0 { get; set; }
        public uint Item_1 { get; set; }
        public uint Item_2 { get; set; }
        public uint Item_3 { get; set; }
        public uint Item_4 { get; set; }
        public uint Item_5 { get; set; }
        public uint Backpack_0 { get; set; }
        public uint Backpack_1 { get; set; }
        public uint Backpack_2 { get; set; }
        [JsonProperty("item_neutral")]
        public uint NeutralItem { get; set; }
    }
}
