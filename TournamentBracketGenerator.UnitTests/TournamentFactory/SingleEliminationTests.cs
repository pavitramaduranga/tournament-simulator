using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.TournamentFactory;

namespace TournamentBracketGenerator.UnitTests.TournamentFactory
{
    [TestFixture]
    public class SingleEliminationTests
    {
        [Test]
        public void SimulateTournament_CallsSingleEliminationStageService()
        {
            // Arrange
            int numberOfTeams = 16; // Adjust as needed
            var singleEliminationStageServiceMock = new Mock<ISingleEliminationStageService>();
            var singleElimination = new SingleElimination(singleEliminationStageServiceMock.Object);

            // Act
            singleElimination.SimulateTournament(numberOfTeams);

            // Assert
            singleEliminationStageServiceMock.Verify(service => service.SimulateTournament(numberOfTeams), Times.Once);
        }
    }
}
