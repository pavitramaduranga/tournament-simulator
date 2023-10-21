namespace TournamentBracketGenerator.Application.Models
{
    public class MatchRound
    {
        public int Round { get; set; }
        public string Type { get; set; }
        public List<MatchEvent> MatchEvents { get; set; }
    }
}
