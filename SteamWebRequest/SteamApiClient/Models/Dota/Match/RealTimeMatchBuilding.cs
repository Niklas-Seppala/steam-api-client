namespace SteamApi.Models.Dota
{
    public class RealTimeMatchBuilding
    {
        public uint Team { get; set; }
        public double Heading { get; set; }
        public uint Type { get; set; }
        public uint Lane { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool Destroyed { get; set; }
    }

}
