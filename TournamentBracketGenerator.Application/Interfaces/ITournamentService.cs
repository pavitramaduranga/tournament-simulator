using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public interface ITournamentService
    {
        public List<Team> teams { get; set; }
        public void AdvanceTeam(List<Team> topTeams);
    }
}