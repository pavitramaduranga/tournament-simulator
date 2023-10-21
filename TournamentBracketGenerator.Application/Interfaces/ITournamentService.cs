using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public interface ITournamentService
    {
        public List<MatchRound> matchRounds { get; set; }
        public List<Team> teams { get; set; }
        public Team? GetTournamentWinner();
        public List<MatchEvent> GetWinnerMatches(Team winningTeam);
        public void AdvanceTeam(List<Team> topTeams);
    }
}