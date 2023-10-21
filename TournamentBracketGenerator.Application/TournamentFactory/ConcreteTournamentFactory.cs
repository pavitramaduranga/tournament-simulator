using TournamentBracketGenerator.Application.Interfaces;

namespace TournamentBracketGenerator.Application.TournamentFactory
{
    internal class ConcreteTournamentFactory : TournamentFactory
    {

        private readonly ISingleEliminationStageService _singleEliminationStageService;
        private readonly IGroupStageService _groupStageService;

        public ConcreteTournamentFactory(ISingleEliminationStageService singleEliminationStageService, IGroupStageService groupStageService)
        {
            _singleEliminationStageService = singleEliminationStageService;
            _groupStageService = groupStageService;
        }

        public override ITournamentFactory GetGameMethodFactory(string tournamentType)
        {
            switch (tournamentType)
            {
                case "SingleElimination":
                    return new SingleElimination(_singleEliminationStageService);
                case "GroupStage":
                    return new GroupStage(_groupStageService);
                default:
                    throw new ApplicationException(string.Format("TournamentType '{0}' cannot be created", tournamentType));
            }
        }
    }
}
