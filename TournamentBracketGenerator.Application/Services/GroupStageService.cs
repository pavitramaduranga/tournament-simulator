using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class GroupStageService : IGroupStageService
    {
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;
        private readonly ILogService _logService;

        public GroupStageService(ITeamService teamService, ITournamentService tournamentService, ILogService logService)
        {
            _teamService = teamService;
            _tournamentService = tournamentService;
            _logService = logService;
        }

        public void SimulateTournament(int numberOfTeams)
        {
            List<Team> teams = _teamService.SeedTeams(numberOfTeams);

            List<List<Team>> groups = BreakIntoTeams(teams);

            _logService.Write("\nTournament Group Stage");
            List<Team> topTeams = SimulateGroupMatchesToGetTopTeamsFromEachGroup(groups);

            _logService.Write("\nTop Teams from the Group Stage Round:");
            foreach (var team in topTeams)
            {
                _logService.Write($"{team.Name} (Seed {team.Seed})");
            }

            _logService.Write("\nMatches in Single Elimination round for the top teams from the Group Stage");

            _tournamentService.AdvanceTeam(topTeams);
        }

        private List<Team> SimulateGroupMatchesToGetTopTeamsFromEachGroup(List<List<Team>> groups)
        {
            List<Team> topTeams = new();
            foreach (var group in groups)
            {
                _logService.Write("\nGroup Teams :");
                foreach (var team in group)
                {
                    _logService.Write(team.Name);
                }

                List<Team> groupTopTeams = GetTopTeams(group, 2);
                topTeams.AddRange(groupTopTeams);
            }

            return topTeams;
        }

        private static List<List<Team>> BreakIntoTeams(List<Team> teams)
        {
            var groups = new List<List<Team>>();
            for (int i = 0; i < teams.Count; i += 4)
            {
                groups.Add(teams.Skip(i).Take(4).ToList());
            }

            return groups;
        }

        private static List<Team> GetTopTeams(List<Team> group, int topCount)
        {
            Dictionary<Team, int> pointsDictionary = new();

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
