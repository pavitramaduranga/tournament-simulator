namespace TournamentBracketGenerator.Application.TournamentFactory
{
    public abstract class TournamentFactory
    {
        public abstract ITournamentFactory GetGameMethodFactory(string tournamentType);

    }
}
