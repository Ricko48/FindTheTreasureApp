namespace FindTheTreasureServer.Database.Entity
{
    public class Beacon : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string PositionDescription { get; set; } = string.Empty;
        public float PositionX { get; set; }
        public float PositionY { get; set; }
    }
}
