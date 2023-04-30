namespace FindTheTreasureServer.Database.Entity
{
    public class GameBeacon : BaseEntity
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public int BeaconId { get; set; }
        public virtual Beacon Beacon { get; set; }
    }
}
