using System;
using System.Collections.Generic;
using System.Linq;

class Team
{
    public string Name { get; set; }
    public int Seed { get; set; }
    public Team NextRoundOpponent { get; set; }
}

class MatchEvent
{
    public string Winner { get; set; }
    public string Loser { get; set; }
    public MatchEvent(string winner, string loser)
    {
        Winner = winner;
        Loser = loser;
    }
}

class Tournament
{
    public List<Team> teams;
    public List<MatchEvent> matchEvents;

    public Tournament()
    {
        teams = new List<Team>();
        matchEvents = new List<MatchEvent>();
    }

    #region Seed data
    public void SeedTeam(int seed, string teamName)
    {
        Team team = new Team { Name = teamName, Seed = seed };
        teams.Add(team);
    }

    public void SeedTeams(int numberOfTeams)
    {
        for (int seed = 1; seed <= numberOfTeams; seed++)
        {
            string teamName = $"Team {seed}A";
            if (seed > numberOfTeams / 2)
            {
                teamName = $"Team {seed - numberOfTeams / 2}B";
            }
            SeedTeam(seed, teamName);
        }
    }
    #endregion

    #region Tournament
    public Team GetTournamentWinner()
    {
        return teams.Count == 1 ? teams[0] : null;
    }

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
        if (random.Next(2) == 0)
        {
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team1.Name} wins");
            matchEvents.Add(new MatchEvent(team1.Name, team2.Name));
            teams.Remove(team2);
            return team1;
        }
        else
        {
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team2.Name} wins");
            matchEvents.Add(new MatchEvent(team2.Name, team1.Name));
            teams.Remove(team1);
            return team2;
        }
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
    }

    public void SimulateTournament()
    {
        while (teams.Count != 1)
        {
            PairTeams();
            var teamList = teams.ToList();
            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                SimulateMatch(team, team.NextRoundOpponent);
            }
        }

        PathToVictory(GetTournamentWinner());
    }

    #endregion

    #region Grouping stage
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

    private int SimulateGroupMatches(Team team, List<Team> group)
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
    }

    #endregion


}

class Program
{
    static void Main()
    {
        Tournament tournament = new Tournament();

        Console.WriteLine("Tournament Dashboard");

        int option = 0;
        while (option != 4)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Simulate Group Stage");
            Console.WriteLine("2. Simulate World cup");
            Console.WriteLine("3. Simulate NCAA soccer");
            Console.WriteLine("4. Exit");
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        tournament.SeedTeams(32);
                        tournament.SimulateGroupStage();
                        break;
                    case 2:
                        tournament.SeedTeams(16);
                        tournament.SimulateTournament();
                        break;
                    case 3:
                        tournament.SeedTeams(64);
                        tournament.SimulateTournament();
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid option.");
                        break;
                }

                tournament.teams.Clear();

            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
            }
        }
    }
}
