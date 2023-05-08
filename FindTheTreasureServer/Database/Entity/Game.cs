using System.ComponentModel.DataAnnotations.Schema;

namespace FindTheTreasureServer.Database.Entity
{
    public class Game : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [ForeignKey("User")]
        public int OwnerId { get; set; }
    }
}
