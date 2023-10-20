using TournamentBracketGenerator.Application.Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("Tournament Dashboard");

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
        Console.WriteLine("1. Simulate Group Stage");
        Console.WriteLine("2. Simulate World Cup Tournament");
        Console.WriteLine("3. Exit");
    }

    private static void HandleUserInput(int option)
    {
        TournamentService tournamentService = new TournamentService();
        //TeamService teamService = new TeamService();

        switch (option)
        {
            case 1:
                tournamentService.SeedTeams(32);
                tournamentService.SimulateGroupStage();
                break;
            case 2:
                tournamentService.SeedTeams(16);
                tournamentService.SimulateTournament();
                break;
            case 3:
                Console.WriteLine("Exiting the program.");
                break;
            default:
                Console.WriteLine("Invalid option. Please select a valid option.");
                break;
        }
    }
}
