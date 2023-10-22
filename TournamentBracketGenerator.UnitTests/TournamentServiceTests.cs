using Moq;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class TournamentServiceTests
    {
        private TournamentService tournamentService;

        [SetUp]
        public void SetUp()
        {
            tournamentService = new TournamentService();
        }

        [Test]
        public void AdvanceTeam_ShouldCreateMatchRounds()
        {
            // Arrange
            List<Team> teams = CreateTeams(8);
            var random = new Random();
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(2)).Returns(0); // Always return 0 for consistent winners

            // Act
            tournamentService.AdvanceTeam(teams);

            // Assert
            Assert.AreEqual(3, tournamentService.matchRounds.Count); // 3 rounds for 8 teams
            CollectionAssert.AllItemsAreInstancesOfType(tournamentService.matchRounds, typeof(MatchRound));
            Assert.AreEqual(8, tournamentService.matchRounds[0].MatchEvents.Count); // First round with 8 matches
        }

        [Test]
        public void GetTournamentWinner_WhenTournamentNotFinished_ShouldReturnNull()
        {
            // Act
            Team? winner = tournamentService.GetTournamentWinner();

            // Assert
            Assert.IsNull(winner);
        }

        [Test]
        public void GetTournamentWinner_WhenTournamentFinished_ShouldReturnWinner()
        {
            // Arrange
            List<Team> teams = CreateTeams(1);
            tournamentService.AdvanceTeam(teams);

            // Act
            Team? winner = tournamentService.GetTournamentWinner();

            // Assert
            Assert.AreEqual(teams[0], winner);
        }

        [Test]
        public void GetWinnerMatches_ShouldReturnMatchesForWinner()
        {
            // Arrange
            List<Team> teams = CreateTeams(2);
            tournamentService.AdvanceTeam(teams);

            // Act
            List<MatchEvent> winnerMatches = tournamentService.GetWinnerMatches(teams[0]);

            // Assert
            Assert.AreEqual(1, winnerMatches.Count);
            Assert.AreEqual(teams[0].Name, winnerMatches[0].Winner);
        }

        private List<Team> CreateTeams(int count)
        {
            var teams = new List<Team>();
            for (int i = 1; i <= count; i++)
            {
                Team team = new()
                {
                    Name = i.ToString(),
                    Seed = i
                };
                teams.Add(team);
            }
            return teams;
        }
    }
}
