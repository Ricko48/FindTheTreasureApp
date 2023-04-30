using FindTheTreasureServer.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace FindTheTreasureServer.Database
{
    public class TreasureDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameParticipant> GameParticipants { get; set; }
        public DbSet<Beacon> Beacons { get; set; }
        public DbSet<GameBeacon> GameBeacons { get; set; }
        public DbSet<ParticipantBeacon> ParticipantBeacons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FindTheTreasure");
        }
    }
}
