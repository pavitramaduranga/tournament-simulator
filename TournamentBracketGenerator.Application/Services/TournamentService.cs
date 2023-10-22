using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class TournamentService : ITournamentService
    {
        public List<MatchRound> MatchRounds { get; set; } = new List<MatchRound>();
        public List<Team> Teams { get; set; } = new List<Team>();

        public void AdvanceTeam(List<Team> topTeams)
        {
            Teams = topTeams;
            int round = 0;
            while (Teams.Count != 1)
            {
                round++;
                PairTeams();
                var matchRound = SimulateMatches(round);
                MatchRounds.Add(matchRound);
            }
        }

        public Team? GetTournamentWinner() => Teams.Count == 1 ? Teams[0] : null;

        public List<MatchEvent> GetWinnerMatches(Team winningTeam)
        {
            return this.MatchRounds
                .SelectMany(round => round.MatchEvents)
                .Where(match => match.Winner == winningTeam?.Name)
                .ToList();
        }

        private MatchEvent SimulateMatch(Team team1, Team team2)
        {
            Random random = new();
            Team winner = random.Next(2) == 0 ? team1 : team2;
            Team loser = winner == team1 ? team2 : team1;
            MatchEvent matchEvent = new(winner.Name, loser.Name);
            Teams.Remove(loser);

            return matchEvent;
        }

        private void PairTeams()
        {
            int totalTeams = Teams.Count;
            int halfTotalTeams = totalTeams / 2;

            for (int i = 0; i < halfTotalTeams; i++)
            {
                Team team1 = Teams[i];
                Team team2 = Teams[totalTeams - i - 1];

                team1.NextRoundOpponent = team2;
                team2.NextRoundOpponent = team1;
            }
            OrderTeamsBySeed();
        }

        private void OrderTeamsBySeed()
        {
            Teams = Teams.OrderBy(team => team.Seed).ToList();
        }

        private MatchRound SimulateMatches(int round)
        {
            var teamList = Teams.ToList();

            MatchRound matchRound = new()
            {
                Round = round,
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
