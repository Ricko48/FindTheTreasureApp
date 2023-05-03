namespace FindTheTreasureServer.Database.Entity
{
    public class GameParticipant : BaseEntity
    {
        public int GameId { get; set; }

        public int UserId { get; set; }
    }
}
