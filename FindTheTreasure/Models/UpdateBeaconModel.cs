using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindTheTreasure.Models
{
    public class UpdateBeaconModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PositionDescription { get; set; } = string.Empty;
        public string Puzzle { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public string MacAddress { get; set; }
        public int? GameId { get; set; }
    }
}
