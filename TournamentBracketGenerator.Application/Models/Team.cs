namespace TournamentBracketGenerator.Application.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Seed { get; set; }
        public Team NextRoundOpponent { get; set; }
    }
}
