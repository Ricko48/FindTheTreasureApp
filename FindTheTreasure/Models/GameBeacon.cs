namespace FindTheTreasure.Models
{
    public class GameBeacon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PositionDescription { get; set; } = string.Empty;
        public string Puzzle { get; set; } = string.Empty;
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public string MacAddress { get; set; } = string.Empty;
        public int? GameId { get; set; }
        public int? Order { get; set; }
    }
}
