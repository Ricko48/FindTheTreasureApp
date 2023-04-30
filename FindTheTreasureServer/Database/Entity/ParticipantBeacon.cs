namespace FindTheTreasureServer.Database.Entity
{
    public class ParticipantBeacon : BaseEntity
    {
        public int GameParticipantId { get; set; }
        public virtual GameParticipant GameParticipant { get; set; }
        public int GameBeaconId { get; set; }
        public virtual GameBeacon GameBeacon { get; set; }
        public bool Found { get; set; }
    }
}
