namespace FindTheTreasure.Models
{
    public class BeaconModel
    {
        public int Id { get; set; }
        public int? GameID { get; set; }
        public string MacAddress { get; set; }
        public string PositionDescription { get; set; }
        public string Name { get; set; }
        public string Puzzle { get; set; }
        public int? Number { get; set; }
        public string SerialNumber { get; set; } = string.Empty;
        public DateTime Discovered { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Order { get; set; }
    }
}
