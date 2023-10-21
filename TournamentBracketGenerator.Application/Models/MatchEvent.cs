namespace TournamentBracketGenerator.Application.Models
{
    public class MatchEvent
    {
        public string Winner { get; set; }
        public string Loser { get; set; }
        public MatchEvent(string winner, string loser)
        {
            Winner = winner;
            Loser = loser;
        }
    }
}
