# Tournament-Simulator
World Cup Tournament Simulator

- The project uses .NET 6.0 runtime environment <br>
- The start-up project is TournamentBracketGenerator.Application <br>
- TournamentBracketGenerator.UnitTests project has test suites <br>

# Design Concerns
- The Factory method is used to load the game type
- Dependency injection is used

# Known Issues

The JSON given with the assignment has 16 countries, which is insufficient considering the 64 team brackets and the grouping stage. Therefore I have used a method to generate team names.

# Class Diagram

![image](https://github.com/pavitramaduranga/tournament-simulator/assets/4363523/bbace911-fb5d-4bef-a712-3cc16c17a3ac)

# Unit Test Coverage

![image](https://github.com/pavitramaduranga/tournament-simulator/assets/4363523/08233f0f-cdde-4d18-bcd9-b7341a5f1c20)

- Unit tests added with NUnit test framework.
- Coverage measurements were taken with a Visual Studio Extension named Fine Code Coverage.
- Application.cs class is excluded from code coverage as it contains only the print methods.
- Program.cs Class cannot be excluded from code coverage due to the structure of the class.

