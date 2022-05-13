namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IGameState
    {
        int RoundInitialCardsCount { get; }

        int DeckMinCardsBeforeClosing { get; }

        int MarriageCardsCount { get; }

        int TrickCardsCount { get; }

        int GameWinPoints { get; }

        int RoundWinPoints { get; }

        int RoundHalfPoints { get; }

        int PlayerWinMaxRoundPoints { get; }

        int PlayerWinHalfRoundPoints { get; }

        int PlayerWinMinRoundPoints { get; }

        int LastTrickWinnerBonusPoints { get; }

        PlayerPosition RoundWinner { get; set; }
    }
}
