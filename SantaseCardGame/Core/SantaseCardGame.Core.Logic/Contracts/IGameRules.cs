namespace SantaseCardGame.Core.Logic.Contracts
{
    public interface IGameRules
    {
        int RoundInitialCardsCount { get; }

        int DeckMinCardsBeforeClosing { get; }

        int GameWinPoints { get; }

        int RoundWinPoints { get; }

        int RoundHalfPoints { get; }

        int PlayerWinMaxRoundPoints { get; }

        int PlayerWinHalfRoundPoints { get; }

        int PlayerWinMinRoundPoints { get; }

        int LastTrickWinnerBonusPoints { get; }
    }
}
