namespace SantaseCardGame.Core.Logic.Rules
{
    using SantaseCardGame.Core.Logic.Contracts;

    public class GameRules : IGameRules
    {
        public int RoundWinPoints => 66;

        public int RoundHalfPoints => 33;

        public int GameWinPoints => 11;

        public int PlayerStartCards => 6;
    }
}