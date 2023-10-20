using System;
using System.Collections.Generic;
using System.Linq;

class Team
{
    public string Name { get; set; }
    public int Seed { get; set; }
    public Team NextRoundOpponent { get; set; }
}

class Tournament
{
    public List<Team> teams; // Make the teams field public

    public Tournament()
    {
        teams = new List<Team>();
    }

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

    public Team AdvanceTeam(Team team)
    {
        if (teams.Contains(team))
        {
            if (teams.Count > 1)
            {
                // Pair teams for the next round
                PairTeams();
            }
            return GetTournamentWinner();
        }
        else
        {
            Console.WriteLine("Team not found in the tournament.");
            return null;
        }
    }

    public Team GetTournamentWinner()
    {
        return teams.Count == 1 ? teams[0] : null;
    }

    public void PathToVictory()
    {
        Console.WriteLine("Tournament Path to Victory:");
        foreach (var team in teams)
        {
            Console.WriteLine($"{team.Name} (Seed {team.Seed})");
        }
    }

    public void SimulateMatch(Team team1, Team team2)
    {
        // Simulate a match between two teams. You can use your own logic here.
        Random random = new Random();
        if (random.Next(2) == 0)
        {
            // Remove the losing team and advance the winning team
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team1.Name} wins");
            teams.Remove(team2);
        }
        else
        {
            // Remove the losing team and advance the winning team
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team2.Name} wins");
            teams.Remove(team1);
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

            // Set the next round opponent for each team
            team1.NextRoundOpponent = team2;
            team2.NextRoundOpponent = team1;
        }
    }
}

class Program
{
    static void Main()
    {
        Tournament tournament = new Tournament();

        // Seed 16 teams
        int numberOfTeams = 16;
        tournament.SeedTeams(numberOfTeams);

        // Simulate the tournament until a winner is found
        while (tournament.teams.Count != 1)
        {
            // Call PairTeams to pair half of the teams for the next round
            tournament.PairTeams();

            // Simulate matches
            var teamList = tournament.teams.ToList(); // Use ToList to avoid collection modification error
            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                tournament.SimulateMatch(team, team.NextRoundOpponent);
            }

        }

        // Simulate the final match (championship match)
        if (tournament.teams.Count == 1)
        {
            Team team1 = tournament.teams[0];
            Team team2 = tournament.teams[0].NextRoundOpponent;

            Console.WriteLine($"Final Match: {team1.Name} vs {team2.Name}");
            tournament.SimulateMatch(team1, team2);
        }


        // Display the path to victory
        tournament.PathToVictory();

        // Display the tournament winner
        Team winner = tournament.GetTournamentWinner();
        Console.WriteLine($"Tournament Winner: {winner.Name} (Seed {winner.Seed})");
    }
}
