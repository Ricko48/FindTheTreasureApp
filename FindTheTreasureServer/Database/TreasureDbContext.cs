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
        public DbSet<ParticipantBeacon> ParticipantBeacons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FindTheTreasure");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "user1", FirstName = "John", LastName = "Doe" },
                new User { Id = 2, UserName = "user2", FirstName = "Jane", LastName = "Smith" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "Game 1", Description = "The first game", OwnerId = 1 },
                new Game { Id = 2, Name = "Game 2", Description = "The second game", OwnerId = 2 }
            );

            modelBuilder.Entity<GameParticipant>().HasData(
                new GameParticipant { Id = 1, GameId = 1, UserId = 1, Start = DateTime.Now },
                new GameParticipant { Id = 2, GameId = 1, UserId = 2, Start = DateTime.Now },
                new GameParticipant { Id = 3, GameId = 2, UserId = 1, Start = DateTime.Now }
            );

            modelBuilder.Entity<Beacon>().HasData(
                new Beacon { Id = 1, Name = "Beacon 1", PositionDescription = "Near the river", PositionX = 1.0f, PositionY = 2.0f, MacAddress = "00:11:22:33:44:55", GameId = 1 },
                new Beacon { Id = 2, Name = "Beacon 2", PositionDescription = "On the hill", PositionX = 3.0f, PositionY = 4.0f, MacAddress = "66:77:88:99:aa:bb", GameId = 1 },
                new Beacon { Id = 3, Name = "Beacon 3", PositionDescription = "In the forest", PositionX = 5.0f, PositionY = 6.0f, MacAddress = "cc:dd:ee:ff:00:11", GameId = 2 }
            );

            modelBuilder.Entity<ParticipantBeacon>().HasData(
                new ParticipantBeacon { Id = 1, GameParticipantId = 1, BeaconId = 1, Found = false },
                new ParticipantBeacon { Id = 2, GameParticipantId = 1, BeaconId = 2, Found = true },
                new ParticipantBeacon { Id = 3, GameParticipantId = 2, BeaconId = 1, Found = true },
                new ParticipantBeacon { Id = 4, GameParticipantId = 3, BeaconId = 3, Found = false }
            );
        }
    }
}
