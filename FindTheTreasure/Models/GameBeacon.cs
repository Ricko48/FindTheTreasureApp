﻿namespace FindTheTreasure.Models
{
    public class GameBeacon
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public string PositionDescription { get; set; }
        public string MacAddress { get; set; }
        public string LocationDescription { get; set; }
        public string Puzzle { get; set; }
    }
}
