using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class TeamServiceTests
    {
        [Test]
        public void SeedTeams_Should_Return_Correct_Number_Of_Teams()
        {
            // Arrange
            var teamService = new TeamService();
            int numberOfTeams = 16;

            // Act
            List<Team> teams = teamService.SeedTeams(numberOfTeams);

            // Assert
            Assert.IsNotNull(teams);
            Assert.That(teams.Count, Is.EqualTo(numberOfTeams));
        }

        [Test]
        public void GenerateTeamName_Should_Return_Correct_TeamName()
        {

            // Arrange
            var teamService = new TeamService();
            int numberOfTeams = 16;

            // Act
            List<Team> teams = teamService.SeedTeams(numberOfTeams);

            // Assert
            Assert.IsNotNull(teams);
            Assert.That(teams?.LastOrDefault()?.Name, Is.EqualTo("Team 16B"));
            Assert.That(teams?.FirstOrDefault()?.Name, Is.EqualTo("Team 1A"));
        }
    }
}
