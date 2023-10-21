using TournamentBracketGenerator.Application.Interfaces;

namespace TournamentBracketGenerator.Application.TournamentFactory
{
    public class SingleElimination : ITournamentFactory
    {
        private readonly ISingleEliminationStageService _singleEliminationStageService;

        public SingleElimination(ISingleEliminationStageService singleEliminationStageService)
        {
            _singleEliminationStageService = singleEliminationStageService;
        }

        public void SimulateTournament(int numberOfTeams)
        {
            _singleEliminationStageService.SimulateTournament(numberOfTeams);
        }
    }
}
