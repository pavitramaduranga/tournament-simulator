using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.UnitTests.TestHelper
{
    public class TeamsHelper
    {
        public static List<Team> CreateSampleTeams(int numberOfTeams)
        {
            var teams = new List<Team>();
            for (int i = 1; i <= numberOfTeams; i++)
            {
                teams.Add(new Team { Name = $"Team {i}", Seed = i });
            }
            return teams;
        }
    }
}
