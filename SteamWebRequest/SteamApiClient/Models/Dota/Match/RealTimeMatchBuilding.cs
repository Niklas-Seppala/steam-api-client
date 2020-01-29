namespace SteamApiClient.Models.Dota
{
    public class RealTimeMatchBuilding
    {
        public byte Team { get; set; }
        public double Heading { get; set; }
        public byte Type { get; set; }
        public byte Lane { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public bool Destroyed { get; set; }
    }

}
