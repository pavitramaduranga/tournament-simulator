using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class GroupStageServiceTests
    {
        private GroupStageService _groupStageService;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<ITournamentService> _tournamentServiceMock;
        private StringWriter _outputCapture;
        private TextWriter _originalOutput;

        [SetUp]
        public void SetUp()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _tournamentServiceMock = new Mock<ITournamentService>();

            _originalOutput = Console.Out;
            _outputCapture = new StringWriter();
            Console.SetOut(_outputCapture);

            _groupStageService = new GroupStageService(_teamServiceMock.Object, _tournamentServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_originalOutput);
            _outputCapture.Dispose();
        }

        [Test]
        public void SimulateTournament_Should_Add_Correct_Console_Lines()
        {
            // Arrange
            int numberOfTeams = 16;
            List<Team> seededTeams = CreateSampleTeams(numberOfTeams);
            _teamServiceMock.Setup(ts => ts.SeedTeams(numberOfTeams)).Returns(seededTeams);

            // Act
            _groupStageService.SimulateTournament(numberOfTeams);

            // Assert
            string capturedOutput = _outputCapture.ToString();
            StringAssert.Contains("Matches in Single Elimination round for the top teams from the Group Stage", capturedOutput);
            StringAssert.Contains("(Seed", capturedOutput);
        }

        [Test]
        public void SimulateTournament_Should_Call_Supporting_Services()
        {
            // Arrange
            int numberOfTeams = 16;
            List<Team> seededTeams = CreateSampleTeams(numberOfTeams);
            _teamServiceMock.Setup(ts => ts.SeedTeams(numberOfTeams)).Returns(seededTeams);

            // Act
            _groupStageService.SimulateTournament(numberOfTeams);

            // Assert
            // Verify that SeedTeams method of the team service was called.
            _teamServiceMock.Verify(ts => ts.SeedTeams(numberOfTeams), Times.Once);

            // Verify that the AdvanceTeam method of the tournament service was called.
            _tournamentServiceMock.Verify(ts => ts.AdvanceTeam(It.IsAny<List<Team>>()), Times.Once);
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
