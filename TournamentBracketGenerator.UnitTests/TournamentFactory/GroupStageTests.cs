using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.TournamentFactory;

namespace TournamentBracketGenerator.UnitTests.TournamentFactory
{
    [TestFixture]
    public class GroupStageTests
    {
        [Test]
        public void SimulateTournament_CallsGroupStageService()
        {
            // Arrange
            int numberOfTeams = 32; // Adjust as needed
            var groupStageServiceMock = new Mock<IGroupStageService>();
            var groupStage = new GroupStage(groupStageServiceMock.Object);

            // Act
            groupStage.SimulateTournament(numberOfTeams);

            // Assert
            groupStageServiceMock.Verify(service => service.SimulateTournament(numberOfTeams), Times.Once);
        }
    }
}
