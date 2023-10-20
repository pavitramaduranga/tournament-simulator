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
        Team winner = GetTournamentWinner();
        if (winner != null)
        {
            List<string> path = new List<string>();
            path.Add($"{winner.Name} (Seed {winner.Seed}) - Tournament Winner");

            foreach (MatchEvent matchEvent in matchEvents)
            {
                path.Add($"{matchEvent.Winner} defeated {matchEvent.Loser}");
            }

            foreach (var step in path)
            {
                Console.WriteLine(step);
            }
        }
    }

    public void SimulateMatch(Team team1, Team team2)
    {
        Random random = new Random();
        if (random.Next(2) == 0)
        {
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team1.Name} wins");
            matchEvents.Add(new MatchEvent(team1.Name, team2.Name));
            teams.Remove(team2);
        }
        else
        {
            Console.WriteLine($"{team1.Name} vs {team2.Name} - {team2.Name} wins");
            matchEvents.Add(new MatchEvent(team2.Name, team1.Name));
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

        int numberOfTeams = 16;
        tournament.SeedTeams(numberOfTeams);

        while (tournament.teams.Count != 1)
        {
            tournament.PairTeams();
            var teamList = tournament.teams.ToList();
            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                tournament.SimulateMatch(team, team.NextRoundOpponent);
            }
        }

        if (tournament.teams.Count == 1)
        {
            Team team1 = tournament.teams[0];
            Team team2 = tournament.teams[0].NextRoundOpponent;
            Console.WriteLine($"Final Match: {team1.Name} vs {team2.Name}");
            tournament.SimulateMatch(team1, team2);
        }

        tournament.PathToVictory();

        Team winner = tournament.GetTournamentWinner();
        Console.WriteLine($"Tournament Winner: {winner.Name} (Seed {winner.Seed})");
    }
}
