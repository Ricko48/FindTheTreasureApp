using FindTheTreasureServer.Database;
using FindTheTreasureServer.Database.Entity;
using System.Collections.Generic;

namespace FindTheTreasureServer
{
    public class Seed
    {
        public static void SeedData(TreasureDbContext context)
        {
            var beacons = new List<Beacon>
            {
                new Beacon { Name = "Beacon 1", PositionX = 1.0f, PositionY = 2.0f, PositionDescription = "FI MUNI A318" },
                new Beacon { Name = "Beacon 2", PositionX = 3.0f, PositionY = 4.0f, PositionDescription = "MZK" },
                new Beacon { Name = "Beacon 3", PositionX = 5.0f, PositionY = 6.0f, PositionDescription = "Tesco Campus pri pečive" }
            };

            var users = new List<User>
            {
                new User { Name = "John", Surname = "Doe" },
                new User { Name = "Jane", Surname = "Doe"},
                new User { Name = "Bob", Surname = "Smith"}
            };

            var games = new List<Game>
            {
                new Game { Name = "Game 1", Description = "Description 1", GameParticipants = new List<GameParticipant>() },
                new Game { Name = "Game 2", Description = "Description 2", GameParticipants = new List<GameParticipant>() }
            };

            var gameParticipants = new List<GameParticipant>
            {
                new GameParticipant { Game = games[0], User = users[0] },
                new GameParticipant { Game = games[0], User = users[1] },
                new GameParticipant { Game = games[1], User = users[2] }
            };

            var gameBeacons = new List<GameBeacon>
            {
                new GameBeacon { Game = games[0], Beacon = beacons[0] },
                new GameBeacon { Game = games[0], Beacon = beacons[1] },
                new GameBeacon { Game = games[1], Beacon = beacons[2] }
            };

            var participantBeacons = new List<ParticipantBeacon>
            {
                new ParticipantBeacon { GameParticipant = gameParticipants[0], GameBeacon = gameBeacons[0], Found = false },
                new ParticipantBeacon { GameParticipant = gameParticipants[0], GameBeacon = gameBeacons[1], Found = true },
                new ParticipantBeacon { GameParticipant = gameParticipants[1], GameBeacon = gameBeacons[0], Found = false },
                new ParticipantBeacon { GameParticipant = gameParticipants[1], GameBeacon = gameBeacons[1], Found = true },
                new ParticipantBeacon { GameParticipant = gameParticipants[2], GameBeacon = gameBeacons[2], Found = false }
            };

            context.Users.AddRange(users);
            context.Beacons.AddRange(beacons);
            context.Games.AddRange(games);
            context.GameBeacons.AddRange(gameBeacons);
            context.GameParticipants.AddRange(gameParticipants);
            context.ParticipantBeacons.AddRange(participantBeacons);
            context.SaveChanges();
        }
    }
}
