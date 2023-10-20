using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Interfaces
{
    public interface ITeamService
    {
        List<Team> SeedTeams(int numberOfTeams);
    }
}
