using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    internal class TournamentService
    {
        private List<Team> teams = new List<Team>();
        private List<MatchEvent> matchEvents = new List<MatchEvent>();



        //todo :: directcly inject and use after moving this to a diferent service
        public void SeedTeams(int numberOfTeams)
        {
            TeamService teamService = new TeamService();
            teams = teamService.SeedTeams(numberOfTeams); // Call SeedTeams and assign the list to the teams field
        }



        public Team? GetTournamentWinner() => teams.Count == 1 ? teams[0] : null;

        public void PathToVictory(Team team)
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Tournament Path to Victory:");
            if (team != null)
            {
                List<MatchEvent> winnerMatches = matchEvents
                    .Where(match => match.Winner == team.Name)
                    .ToList();

                foreach (MatchEvent matchEvent in winnerMatches)
                {
                    Console.WriteLine($"{matchEvent.Winner} defeated {matchEvent.Loser}");
                }
            }
        }

        public Team SimulateMatch(Team team1, Team team2)
        {
            Random random = new Random();
            Team winner = random.Next(2) == 0 ? team1 : team2;
            Team loser = winner == team1 ? team2 : team1;

            Console.WriteLine($"{team1.Name} vs {team2.Name} - {winner.Name} wins");
            matchEvents.Add(new MatchEvent(winner.Name, loser.Name));
            teams.Remove(loser);

            return winner;
        }

        public void PairTeams()
        {
            int totalTeams = teams.Count;
            int halfTotalTeams = totalTeams / 2;

            for (int i = 0; i < halfTotalTeams; i++)
            {
                Team team1 = teams[i];
                Team team2 = teams[totalTeams - i - 1];

                team1.NextRoundOpponent = team2;
                team2.NextRoundOpponent = team1;
            }

            teams = teams.OrderBy(team => team.Seed).ToList(); // Ensure teams are ordered by seed
        }

        public void SimulateTournament()
        {
            while (teams.Count != 1)
            {
                PairTeams();
                SimulateMatches();
            }

            PathToVictory(GetTournamentWinner());
            ClearTournamentData();
        }

        private void SimulateMatches()
        {
            var teamList = teams.ToList();
            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                SimulateMatch(team, team.NextRoundOpponent);
            }
        }

        public void ClearTournamentData()
        {
            teams.Clear();
            matchEvents.Clear();
        }

        public List<Team> GetTopTeams(List<Team> group, int topCount)
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

        public void SimulateGroupStage()
        {
            // Break the teams into 4 groups of 4 teams each
            var groups = new List<List<Team>>();
            for (int i = 0; i < teams.Count; i += 4)
            {
                groups.Add(teams.Skip(i).Take(4).ToList());
            }

            Console.WriteLine("Group Stage");

            // Simulate group matches and get the top 2 teams from each group
            List<Team> topTeams = new List<Team>();
            foreach (var group in groups)
            {
                Console.WriteLine("Group Teams:");
                foreach (var team in group)
                {
                    Console.WriteLine(team.Name);
                }

                List<Team> groupTopTeams = GetTopTeams(group, 2);
                topTeams.AddRange(groupTopTeams);
            }

            Console.WriteLine("\nTop Teams from the Group Stage:");
            foreach (var team in topTeams)
            {
                Console.WriteLine($"{team.Name} (Seed {team.Seed})");
            }
            this.teams = topTeams; // Assign the top teams to the tournament

            Console.WriteLine("\nTop Teams from the Group Stage in Single Elimination round ");
            SimulateTournament();
        }
    }
}
