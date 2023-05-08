namespace FindTheTreasureServer.Database.Entity
{
    public class GameParticipant : BaseEntity
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
