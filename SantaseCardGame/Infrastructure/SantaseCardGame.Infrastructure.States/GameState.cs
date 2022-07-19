namespace SantaseCardGame.Infrastructure.States
{
    using System;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class GameState : IGameState
    {
        public string CurrentGameId { get; set; }

        public int RoundInitialCardsCount => 6;

        public int DeckMinCardsBeforeClosing => 4;

        public int MarriageCardsCount => 2;

        public int TrickCardsCount => 2;

        public int GameWinPoints => 11;

        public int RoundWinPoints => 66;

        public int RoundHalfPoints => 33;

        public int PlayerWinMaxRoundPoints => 3;

        public int PlayerWinHalfRoundPoints => 2;

        public int PlayerWinMinRoundPoints => 1;

        public int LastTrickWinnerBonusPoints => 10;

        public PlayerPosition RoundWinner { get; set; }

        public event Action<PlayerPosition, string> OnShowMessage;

        public event Action OnEndRound;

        public void ShowMessage(PlayerPosition position, string message) =>
            OnShowMessage?.Invoke(position, message);

        public void EndRound() =>
            OnEndRound?.Invoke();

        public void Clear() =>
            CurrentGameId = string.Empty;
    }
}
