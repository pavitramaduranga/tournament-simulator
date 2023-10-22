using Moq;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;
using TournamentBracketGenerator.UnitTests.TestHelper;

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
            List<Team> teams = TeamsHelper.CreateSampleTeams(16);
            var random = new Random();
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(2)).Returns(0); // Always return 0 for consistent winners

            // Act
            tournamentService.AdvanceTeam(teams);

            // Assert
            Assert.AreEqual(4, tournamentService.MatchRounds.Count); // 4 rounds for 16 teams
            CollectionAssert.AllItemsAreInstancesOfType(tournamentService.MatchRounds, typeof(MatchRound));
            Assert.AreEqual(8, tournamentService.MatchRounds[0].MatchEvents.Count); // First round with 8 matches
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
            List<Team> teams = TeamsHelper.CreateSampleTeams(1);
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
            List<Team> teams = TeamsHelper.CreateSampleTeams(16);
            tournamentService.AdvanceTeam(teams);
            Team? winner = tournamentService.GetTournamentWinner();
            // Act
            List<MatchEvent> winnerMatches = tournamentService.GetWinnerMatches(winner);

            // Assert
            Assert.AreEqual(4, winnerMatches.Count);
            Assert.AreEqual(winner.Name, winnerMatches[0].Winner);
        }
    }
}
