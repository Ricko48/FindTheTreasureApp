namespace FindTheTreasureServer.DTO
{
    public class ScoreboardDTO
    {
        public ICollection<ScoreDTO> Scores { get; set; }
        public string GameName { get; set; }
    }
}
