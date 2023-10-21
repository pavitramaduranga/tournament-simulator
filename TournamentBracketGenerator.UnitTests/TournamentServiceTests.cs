using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class TournamentServiceTests
    {

        private TournamentService _tournamentService;
        private Mock<ILogService> _logServiceMock;
        private StringWriter _outputCapture;
        private TextWriter _originalOutput;

        [SetUp]
        public void SetUp()
        {
            _logServiceMock = new Mock<ILogService>();
            _tournamentService = new TournamentService(_logServiceMock.Object);

            _originalOutput = Console.Out;
            _outputCapture = new StringWriter();
            Console.SetOut(_outputCapture);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_originalOutput);
            _outputCapture.Dispose();
        }

        [Test]
        public void AdvanceTeam_WithTwoTeams_ShouldDetermineWinner()
        {
            // Arrange
            Team team1 = new Team { Name = "Team 1", Seed = 1 };
            Team team2 = new Team { Name = "Team 2", Seed = 2 };
            List<Team> teams = new List<Team> { team1, team2 };

            // Act
            _tournamentService.AdvanceTeam(teams);

            // Assert
            string capturedOutput = _outputCapture.ToString();
            StringAssert.Contains("Tournament Path to Victory:", capturedOutput);
            StringAssert.Contains("defeated Team", capturedOutput);
        }
    }
}
