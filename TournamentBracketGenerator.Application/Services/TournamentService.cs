using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private List<Team> teams = new();
        private List<MatchEvent> matchEvents = new();
        private readonly ILogService _logService;

        public TournamentService(ILogService logService)
        {
            _logService = logService;
        }

        public void AdvanceTeam(List<Team> topTeams)
        {
            teams = topTeams;
            int round = 0;
            while (teams.Count != 1)
            {
                round++;
                _logService.Write("Round " + round);
                PairTeams();
                SimulateMatches();
            }

            PathToVictory(GetTournamentWinner());
            ClearTournamentData();
        }

        private Team? GetTournamentWinner() => teams.Count == 1 ? teams[0] : null;

        private void PathToVictory(Team team)
        {
            _logService.Write("\nTournament Path to Victory:");
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

        private void SimulateMatch(Team team1, Team team2)
        {
            Random random = new Random();
            Team winner = random.Next(2) == 0 ? team1 : team2;
            Team loser = winner == team1 ? team2 : team1;

            _logService.Write($"{team1.Name} vs {team2.Name} - {winner.Name} wins");
            matchEvents.Add(new MatchEvent(winner.Name, loser.Name));
            teams.Remove(loser);
        }

        private void PairTeams()
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

        private void SimulateMatches()
        {
            var teamList = teams.ToList();
            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                SimulateMatch(team, team.NextRoundOpponent);
            }
        }

        private void ClearTournamentData()
        {
            teams.Clear();
            matchEvents.Clear();
        }

    }
}
