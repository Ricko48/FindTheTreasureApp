using FindTheTreasureServer.Database.Entity;
using FindTheTreasureServer.Database;

namespace FindTheTreasureServer
{
    public static class Seed
    {
        public static void SeedData(TreasureDbContext context)
        {
            if (context.Users.Any())
            {
                // Database has already been seeded
                return;
            }

            // Add users
            var users = new[]
            {
                new User {UserName = "user1", FirstName = "John", LastName = "Doe"},
                new User {UserName = "user2", FirstName = "Jane", LastName = "Doe"}
            };
            context.Users.AddRange(users);

            // Add games
            var games = new[]
            {
                new Game {Name = "Game 1", Description = "Description for game 1", OwnerId = 1},
                new Game {Name = "Game 2", Description = "Description for game 2", OwnerId = 2}
            };
            context.Games.AddRange(games);

            // Add game participants
            var gameParticipants = new[]
            {
                new GameParticipant {GameId = 1, UserId = 1, Start = DateTime.UtcNow},
                new GameParticipant {GameId = 1, UserId = 2, Start = DateTime.UtcNow},
                new GameParticipant {GameId = 2, UserId = 1, Start = DateTime.UtcNow}
            };
            context.GameParticipants.AddRange(gameParticipants);

            // Add beacons
            var beacons = new[]
            {
                new Beacon
                {
                    Name = "Real beacon",
                    PositionDescription = "Description for position 1",
                    PositionX = 1.0f,
                    PositionY = 2.0f,
                    MacAddress = "0C:F3:EE:B8:DD:0A"
                },
                new Beacon
                {
                    Name = "Beacon 2",
                    PositionDescription = "Description for position 2",
                    PositionX = 2.0f,
                    PositionY = 3.0f,
                    MacAddress = "11:22:33:44:55:66",
                    GameId = 1,
                    Order = 0,
                },
                new Beacon
                {
                    Name = "Beacon 3",
                    PositionDescription = "Description for position 3",
                    PositionX = 3.0f,
                    PositionY = 4.0f,
                    MacAddress = "22:33:44:55:66:77",
                    GameId = 2,
                    Order = 0,

                }
            };
            context.Beacons.AddRange(beacons);

            // Add participant beacons
            var participantBeacons = new[]
            {
                new ParticipantBeacon {GameParticipantId = 1, BeaconId = 1, Found = true},
                new ParticipantBeacon {GameParticipantId = 1, BeaconId = 2, Found = true},
                new ParticipantBeacon {GameParticipantId = 2, BeaconId = 1, Found = true},
                new ParticipantBeacon {GameParticipantId = 2, BeaconId = 2, Found = false},
                new ParticipantBeacon {GameParticipantId = 3, BeaconId = 3, Found = false}
            };
            context.ParticipantBeacons.AddRange(participantBeacons);

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
