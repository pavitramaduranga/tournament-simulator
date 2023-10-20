using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class TeamService : ITeamService
    {
        public List<Team> SeedTeams(int numberOfTeams)
        {
            List<Team> teams = new();
            for (int seed = 1; seed <= numberOfTeams; seed++)
            {
                string teamName = GenerateTeamName(seed, numberOfTeams);
                Team team = new() { Name = teamName, Seed = seed };
                teams.Add(team);
            }
            return teams;
        }

        private string GenerateTeamName(int seed, int numberOfTeams)
        {
            char group = (char)('A' + (seed - 1) / (numberOfTeams / 2));
            return $"Team {seed}{group}";
        }
    }
}
