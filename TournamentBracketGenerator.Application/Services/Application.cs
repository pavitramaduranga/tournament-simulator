using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.TournamentFactory;

namespace TournamentBracketGenerator.Application.Services
{
    public class Application : IApplication
    {
        private readonly ISingleEliminationStageService _singleEliminationStageService;
        private readonly IGroupStageService _groupStageService;

        public Application(ISingleEliminationStageService singleEliminationStageService, IGroupStageService groupStageService)
        {
            _singleEliminationStageService = singleEliminationStageService;
            _groupStageService = groupStageService;
        }

        public void Main()
        {
            Console.WriteLine("****Tournament Dashboard****");

            int option = 0;
            while (option != 3)
            {
                DisplayMenu();
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    HandleUserInput(option);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Simulate World Cup Tournament");
            Console.WriteLine("2. Simulate Group Stage");
            Console.WriteLine("3. Exit");
        }

        private void HandleUserInput(int option)
        {
            ITournamentFactory? gameApproach = null;
            int numberOfTeams = 0;
            switch (option)
            {
                case 1:
                    gameApproach = CreateTournamentFactory().GetGameMethodFactory("SingleElimination");
                    numberOfTeams = 16;
                    break;
                case 2:
                    gameApproach = CreateTournamentFactory().GetGameMethodFactory("GroupStage");
                    numberOfTeams = 64;
                    break;
                case 3:
                    Console.WriteLine("Exiting application.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        
            gameApproach?.SimulateTournament(numberOfTeams);
        }

        private TournamentFactory.TournamentFactory CreateTournamentFactory()
        {
            return new ConcreteTournamentFactory(_singleEliminationStageService, _groupStageService);
        }
    }
}
