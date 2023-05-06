namespace FindTheTreasure.Models
{
    public class BeaconModel
    {
        public int GameID { get; set; }
        public string MAC { get; set; }        
        public string Name { get; set; }
        public int? Number { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Discovered { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
