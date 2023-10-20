using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;

namespace TournamentBracketGenerator.Application.Services
{
    public class SingleEliminationStageService: ISingleEliminationStageService
    {
        public void SimulateTournament(int numberOfTeams)
        {
            TeamService teamService = new TeamService();
            TournamentService tournamentService = new TournamentService();

            List<Team> teams = teamService.SeedTeams(numberOfTeams);
            tournamentService.AdvanceTeam(teams);
        }
    }
}
