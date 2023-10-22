using TournamentBracketGenerator.Application.Services;

namespace TournamentBracketGenerator.UnitTests
{
    [TestFixture]
    public class LogServiceTests
    {
        [Test]
        public void Write_WritesMessageToConsole()
        {
            // Arrange
            LogService logService = new LogService();
            string message = "Test message";
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            logService.Write(message);
            string consoleOutput = sw.ToString().Trim(); // Get the console output

            // Assert
            Assert.AreEqual(message, consoleOutput);
        }
    }
}
