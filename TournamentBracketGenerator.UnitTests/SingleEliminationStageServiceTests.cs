using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class SingleEliminationStageServiceTests
    {
        private SingleEliminationStageService _singleEliminationService;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<ITournamentService> _tournamentServiceMock;

        [SetUp]
        public void SetUp()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _tournamentServiceMock = new Mock<ITournamentService>();

            _singleEliminationService = new SingleEliminationStageService(_teamServiceMock.Object, _tournamentServiceMock.Object);
        }

        //[Test]
        //public void SimulateTournament_Should_Add_Correct_Console_Lines()
        //{
        //    // Arrange
        //    int numberOfTeams = 16;
        //    List<Team> seededTeams = CreateSampleTeams(numberOfTeams);
        //    _teamServiceMock.Setup(ts => ts.SeedTeams(numberOfTeams)).Returns(seededTeams);

        //    // Act
        //    _singleEliminationService.SimulateTournament(numberOfTeams);

        //    // Assert
        //    string capturedOutput = _outputCapture.ToString();
        //    StringAssert.Contains("Tournament Path to Victory", capturedOutput);
        //    StringAssert.Contains("defeated", capturedOutput);
        //}

        [Test]
        public void SimulateTournament_Should_Seed_Teams_And_Advance()
        {
            // Arrange
            int numberOfTeams = 16;
            List<Team> seededTeams = CreateSampleTeams(numberOfTeams);
            _teamServiceMock.Setup(ts => ts.SeedTeams(numberOfTeams)).Returns(seededTeams);

            // Act
            _singleEliminationService.SimulateTournament(numberOfTeams);

            // Assert
            _tournamentServiceMock.Verify(ts => ts.AdvanceTeam(seededTeams), Times.Once);
        }

        [Test]
        public void SimulateTournament_Should_Handle_Zero_Teams()
        {
            // Arrange
            int numberOfTeams = 0;

            // Act
            _singleEliminationService.SimulateTournament(numberOfTeams);

            // Assert
            _teamServiceMock.Verify(ts => ts.SeedTeams(numberOfTeams), Times.Never);
            _tournamentServiceMock.Verify(ts => ts.AdvanceTeam(It.IsAny<List<Team>>()), Times.Never);
        }

        [Test]
        public void SimulateTournament_Should_Handle_Negative_Teams()
        {
            // Arrange
            int numberOfTeams = -5;

            // Act
            _singleEliminationService.SimulateTournament(numberOfTeams);

            // Assert
            _teamServiceMock.Verify(ts => ts.SeedTeams(numberOfTeams), Times.Never);
            _tournamentServiceMock.Verify(ts => ts.AdvanceTeam(It.IsAny<List<Team>>()), Times.Never);
        }

        private List<Team> CreateSampleTeams(int numberOfTeams)
        {
            var teams = new List<Team>();
            for (int i = 1; i <= numberOfTeams; i++)
            {
                teams.Add(new Team { Name = $"Team {i}", Seed = i });
            }
            return teams;
        }

    }
}