﻿using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.Services;
using TournamentBracketGenerator.UnitTests.TestHelper;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class GroupStageServiceTests
    {
        private GroupStageService _groupStageService;
        private Mock<ITeamService> _teamServiceMock;
        private Mock<ITournamentService> _tournamentServiceMock;
        private Mock<ILogService> _logServiceMock;

        [SetUp]
        public void SetUp()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _tournamentServiceMock = new Mock<ITournamentService>();
            _logServiceMock = new Mock<ILogService>();
            _groupStageService = new GroupStageService(_teamServiceMock.Object, _tournamentServiceMock.Object, _logServiceMock.Object);
        }

        [Test]
        public void SimulateTournament_Should_Call_Supporting_Services()
        {
            // Arrange
            int numberOfTeams = 16;
            List<Team> seededTeams = TeamsHelper.CreateSampleTeams(numberOfTeams);
            _teamServiceMock.Setup(ts => ts.SeedTeams(numberOfTeams)).Returns(seededTeams);

            // Act
            _groupStageService.SimulateTournament(numberOfTeams);

            // Assert
            // Verify that SeedTeams method of the team service was called.
            _teamServiceMock.Verify(ts => ts.SeedTeams(numberOfTeams), Times.Once);

            // Verify that the AdvanceTeam method of the tournament service was called.
            _tournamentServiceMock.Verify(ts => ts.AdvanceTeam(It.IsAny<List<Team>>()), Times.Once);
        }
    }
}
