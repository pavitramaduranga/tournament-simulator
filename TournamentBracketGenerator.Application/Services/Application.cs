﻿using System.Diagnostics.CodeAnalysis;
using TournamentBracketGenerator.Application.Interfaces;
using TournamentBracketGenerator.Application.Models;
using TournamentBracketGenerator.Application.TournamentFactory;

namespace TournamentBracketGenerator.Application.Services
{
    [ExcludeFromCodeCoverage]
    public class Application : IApplication
    {
        private readonly ISingleEliminationStageService _singleEliminationStageService;
        private readonly IGroupStageService _groupStageService;
        private readonly ITournamentService _tournamentService;

        public Application(ISingleEliminationStageService singleEliminationStageService, IGroupStageService groupStageService, ITournamentService tournamentService)
        {
            _singleEliminationStageService = singleEliminationStageService;
            _groupStageService = groupStageService;
            _tournamentService = tournamentService;
        }

        public void Main()
        {
            Console.WriteLine("****Tournament Dashboard****");

            int option = 0;
            while (option != 4)
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

        private TournamentFactory.TournamentFactory CreateTournamentFactory()
        {
            return new ConcreteTournamentFactory(_singleEliminationStageService, _groupStageService);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Simulate World Cup Tournament");
            Console.WriteLine("2. Simulate NCAA Soccer Tournament");
            Console.WriteLine("3. Simulate Group Stage");
            Console.WriteLine("4. Exit");
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
                    gameApproach = CreateTournamentFactory().GetGameMethodFactory("SingleElimination");
                    numberOfTeams = 64;
                    break;
                case 3:
                    gameApproach = CreateTournamentFactory().GetGameMethodFactory("GroupStage");
                    numberOfTeams = 64;
                    break;
                case 4:
                    Console.WriteLine("Exiting application.");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }

            if (option == 1 || option == 2 || option == 3)
            {
                gameApproach?.SimulateTournament(numberOfTeams);
                PrintMatchList();
                PathToVictory();
            }
        }

        private void PrintMatchList()
        {
            if (_tournamentService != null)
            {
                Console.WriteLine("Match Results:");
                Console.WriteLine("{0,-15} {1,-15} {2,-15}", "Winner", "Loser", "Result");

                foreach (var matchRound in _tournamentService.MatchRounds)
                {
                    Console.WriteLine($"Round {matchRound.Round}");

                    foreach (var matchEvent in matchRound.MatchEvents)
                    {
                        string winner = matchEvent.Winner;
                        string loser = matchEvent.Loser;
                        string result = $"{matchEvent.Winner} wins";

                        Console.WriteLine("{0,-15} {1,-15} {2,-15}", winner, loser, result);
                    }
                }
            }
        }

        private void PathToVictory()
        {
            if (_tournamentService != null)
            {
                Team? winningTeam = _tournamentService.GetTournamentWinner();
                Console.WriteLine("\nWinner of the Tournament is " + winningTeam?.Name);
                Console.WriteLine("\nTournament Path to Victory:");

                List<MatchEvent> winnerMatches = _tournamentService.GetWinnerMatches(winningTeam);

                Console.WriteLine("{0,-15} {1,-15}", "Winner", "Defeated");

                foreach (MatchEvent matchEvent in winnerMatches)
                {
                    string winner = matchEvent.Winner;
                    string defeated = matchEvent.Loser;

                    Console.WriteLine("{0,-15} {1,-15}", winner, defeated);
                }
                _tournamentService.MatchRounds.Clear();
                _tournamentService.Teams.Clear();
            }
        }
    }
}
