namespace FindTheTreasureServer.Database.Entity
{
    public class GameParticipant : BaseEntity
    {
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
