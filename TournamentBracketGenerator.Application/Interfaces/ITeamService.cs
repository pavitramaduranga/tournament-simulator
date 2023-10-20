using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Interfaces
{
    public interface ITeamService
    {
        public List<Team> SeedTeams(int numberOfTeams);
    }
}
