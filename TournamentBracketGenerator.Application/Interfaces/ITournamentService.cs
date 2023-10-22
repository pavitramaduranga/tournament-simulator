using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public interface ITournamentService
    {
        public List<MatchRound> MatchRounds { get; set; }
        public List<Team> Teams { get; set; }
        public Team? GetTournamentWinner();
        public List<MatchEvent> GetWinnerMatches(Team winningTeam);
        public void AdvanceTeam(List<Team> topTeams);
    }
}