using TournamentBracketGenerator.Application.Interfaces;

namespace TournamentBracketGenerator.Application.TournamentFactory
{
    public class GroupStage : ITournamentFactory
    {
        private readonly IGroupStageService _groupStageService;

        public GroupStage(IGroupStageService groupStageService)
        {
            _groupStageService = groupStageService;
        }
        public void SimulateTournament(int numberOfTeams)
        {
            _groupStageService.SimulateTournament(numberOfTeams);
        }
    }
}
