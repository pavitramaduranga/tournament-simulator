using Moq;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.TournamentFactory;

namespace TournamentBracketGenerator.UnitTests.TournamentFactory
{
    [TestFixture]
    public class ConcreteTournamentFactoryTests
    {
        [Test]
        public void GetGameMethodFactory_ReturnsSingleEliminationFactory()
        {
            // Arrange
            ISingleEliminationStageService singleEliminationService = new Mock<ISingleEliminationStageService>().Object;
            IGroupStageService groupStageService = new Mock<IGroupStageService>().Object;
            ConcreteTournamentFactory factory = new ConcreteTournamentFactory(singleEliminationService, groupStageService);

            // Act
            ITournamentFactory tournamentFactory = factory.GetGameMethodFactory("SingleElimination");

            // Assert
            Assert.IsInstanceOf<SingleElimination>(tournamentFactory);
        }

        [Test]
        public void GetGameMethodFactory_ReturnsGroupStageFactory()
        {
            // Arrange
            ISingleEliminationStageService singleEliminationService = new Mock<ISingleEliminationStageService>().Object;
            IGroupStageService groupStageService = new Mock<IGroupStageService>().Object;
            ConcreteTournamentFactory factory = new ConcreteTournamentFactory(singleEliminationService, groupStageService);

            // Act
            ITournamentFactory tournamentFactory = factory.GetGameMethodFactory("GroupStage");

            // Assert
            Assert.IsInstanceOf<GroupStage>(tournamentFactory);
        }

        [Test]
        public void GetGameMethodFactory_ThrowsExceptionForInvalidTournamentType()
        {
            // Arrange
            ISingleEliminationStageService singleEliminationService = new Mock<ISingleEliminationStageService>().Object;
            IGroupStageService groupStageService = new Mock<IGroupStageService>().Object;
            ConcreteTournamentFactory factory = new ConcreteTournamentFactory(singleEliminationService, groupStageService);

            // Act and Assert
            Assert.Throws<ApplicationException>(() => factory.GetGameMethodFactory("InvalidTournamentType"));
        }
    }
}
