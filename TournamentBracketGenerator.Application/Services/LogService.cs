using TournamentBracketGenerator.Application.Interfaces;

namespace TournamentBracketGenerator.Application.Services
{
    public class LogService : ILogService
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
