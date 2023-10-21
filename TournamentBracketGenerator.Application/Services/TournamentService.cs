using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class TournamentService : ITournamentService
    {
        public List<MatchRound> matchRounds { get; set; } = new List<MatchRound>();
        public List<Team> teams { get; set; } = new List<Team>();

        public void AdvanceTeam(List<Team> topTeams)
        {
            teams = topTeams;
            int round = 0;
            while (teams.Count != 1)
            {
                round++;
                PairTeams();
                var matchRound = SimulateMatches(round);
                matchRounds.Add(matchRound);
            }
        }

        public Team? GetTournamentWinner() => teams.Count == 1 ? teams[0] : null;

        private MatchEvent SimulateMatch(Team team1, Team team2)
        {
            Random random = new Random();
            Team winner = random.Next(2) == 0 ? team1 : team2;
            Team loser = winner == team1 ? team2 : team1;
            MatchEvent matchEvent = new(winner.Name, loser.Name);
            teams.Remove(loser);

            return matchEvent;
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

        private MatchRound SimulateMatches(int round)
        {
            var teamList = teams.ToList();

            MatchRound matchRound = new MatchRound
            {
                Round = round,
                Type = "SingleElimination",
                MatchEvents = new List<MatchEvent>()
            };

            for (int i = 0; i < teamList.Count / 2; i++)
            {
                var team = teamList[i];
                MatchEvent matchEvent = SimulateMatch(team, team.NextRoundOpponent);
                matchRound.MatchEvents.Add(matchEvent);
            }
            return matchRound;
        }

    }
}
