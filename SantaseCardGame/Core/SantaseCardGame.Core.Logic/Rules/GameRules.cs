namespace SantaseCardGame.Core.Logic.Rules
{
    using SantaseCardGame.Core.Logic.Contracts;

    public class GameRules : IGameRules
    {
        public int RoundInitialCardsCount => 6;
    }
}
