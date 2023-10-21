using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public interface ITournamentService
    {
        public List<MatchRound> matchRounds { get; set; }
        public List<Team> teams { get; set; }
        public Team? GetTournamentWinner();
        public void AdvanceTeam(List<Team> topTeams);
    }
}