using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class GroupStageService : IGroupStageService
    {
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public GroupStageService(ITeamService teamService, ITournamentService tournamentService)
        {
            _teamService = teamService;
            _tournamentService = tournamentService;
        }

        public void SimulateTournament(int numberOfTeams)
        {
            List<Team> teams = _teamService.SeedTeams(numberOfTeams);

            // Break the teams into 4 groups of 4 teams each
            var groups = new List<List<Team>>();
            for (int i = 0; i < teams.Count; i += 4)
            {
                groups.Add(teams.Skip(i).Take(4).ToList());
            }

            Console.WriteLine("\nTournament Group Stage");

            // Simulate group matches and get the top 2 teams from each group
            List<Team> topTeams = new List<Team>();
            foreach (var group in groups)
            {
                Console.WriteLine("\nGroup Teams :");
                foreach (var team in group)
                {
                    Console.WriteLine(team.Name);
                }

                List<Team> groupTopTeams = GetTopTeams(group, 2);
                topTeams.AddRange(groupTopTeams);
            }

            Console.WriteLine("\nTop Teams from the Group Stage Round:");
            foreach (var team in topTeams)
            {
                Console.WriteLine($"{team.Name} (Seed {team.Seed})");
            }

            Console.WriteLine("\nTop Teams from the Group Stage in Single Elimination round ");

            _tournamentService.AdvanceTeam(topTeams);
        }

        private static List<Team> GetTopTeams(List<Team> group, int topCount)
        {
            Dictionary<Team, int> pointsDictionary = new Dictionary<Team, int>();

            foreach (Team team in group)
            {
                int points = SimulateGroupMatches(team, group);
                pointsDictionary[team] = points;
            }

            var sortedTeams = pointsDictionary.OrderByDescending(pair => pair.Value).Select(pair => pair.Key);

            List<Team> topTeams = sortedTeams.Take(topCount).ToList();

            return topTeams;
        }

        private static int SimulateGroupMatches(Team team, List<Team> group)
        {
            int points = 0;

            foreach (Team opponent in group)
            {
                if (team != opponent)
                {
                    Random random = new Random();
                    if (random.Next(2) == 0)
                    {
                        points += 3;
                    }
                    else
                    {
                        points += 1;
                    }
                }
            }

            return points;
        }

    }
}
