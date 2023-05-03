namespace FindTheTreasureServer.Database.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }
}
