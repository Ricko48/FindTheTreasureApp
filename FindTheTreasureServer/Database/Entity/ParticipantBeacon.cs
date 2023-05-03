namespace FindTheTreasureServer.Database.Entity
{
    public class ParticipantBeacon : BaseEntity
    {
        public int GameParticipantId { get; set; }
        public int BeaconId { get; set; }
        public bool Found { get; set; }
    }
}
