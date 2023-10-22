# Tournament-Simulator
World Cup tournament simulator

- The project uses .Net 6 runtime environment <br>
- The start-up project is TournamentBracketGenerator.Application <br>
- TournamentBracketGenerator.UnitTests project has test suites <br>

# Design Concerns
- The Factory method is used to load the game type
- Dependency injection is used

# Known Issues

The JSON given with the assignment has 16 countries, which is insufficient considering the 64 team brackets and the grouping stage. Therefore I have used a method to generate team names.

# Class Diagram

![image](https://github.com/pavitramaduranga/tournament-simulator/assets/4363523/bbace911-fb5d-4bef-a712-3cc16c17a3ac)
