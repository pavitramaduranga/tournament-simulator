using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    internal class TournamentService
    {
        private List<Team> teams = new List<Team>();
        private List<MatchEvent> matchEvents = new List<MatchEvent>();

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

        public void SimulateTournament(int numberOfTeams)
        {
            TeamService teamService = new TeamService();
            teams = teamService.SeedTeams(numberOfTeams);
            AdvanceTeam();
        }

        private void AdvanceTeam()
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

        internal void SimulateTournamentFromTopTeams(List<Team> topTeams)
        {
            teams = topTeams;
            AdvanceTeam();
        }
    }
}
