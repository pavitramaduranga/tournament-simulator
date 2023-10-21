using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public interface ITournamentService
    {
        public void AdvanceTeam(List<Team> topTeams);
    }
}