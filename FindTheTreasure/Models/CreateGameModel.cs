using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Models
{
    public class CreateGameModel
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public virtual ICollection<GameParticipant> GameParticipants { get; set; }
        public virtual ICollection<GameBeacon> GameBeacons { get; set; }

    }
}
