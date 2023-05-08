using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Pages.ScoreBoard.Models
{
    public class ScoreBoard
    {
        public ICollection<Score> Scores { get; set; }
    }
}
