using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class SingleEliminationStageService: ISingleEliminationStageService
    {
        private readonly ITeamService _teamService;
        private readonly ITournamentService _tournamentService;

        public SingleEliminationStageService(ITeamService teamService, ITournamentService tournamentService)
        {
            _teamService = teamService;
            _tournamentService = tournamentService;
        }
        public void SimulateTournament(int numberOfTeams)
        {
            List<Team> teams = _teamService.SeedTeams(numberOfTeams);
            _tournamentService.AdvanceTeam(teams);
        }
    }
}
