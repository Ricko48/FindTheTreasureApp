using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace FindTheTreasure.Models
{
    public class GameParticipant
    {
        public int GameId { get; set; }
        public virtual CreateGameModel Game { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
