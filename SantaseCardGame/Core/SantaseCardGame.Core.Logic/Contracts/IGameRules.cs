namespace SantaseCardGame.Core.Logic.Contracts
{
    public interface IGameRules
    {
        public int RoundWinPoints { get; }

        public int RoundHalfPoints { get; }

        public int GameWinPoints { get; }

        public int PlayerStartCards { get; }
    }
}