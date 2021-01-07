namespace SantaseCardGame.Infrastructure.States
{
    using System;

    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class GameState : IGameState
    {
        public event Action OnEndRound;

        public event Action OnRenderBoard;

        public event Action<string> OnShowMessage;

        public int RoundWinPoints => 66;

        public int RoundHalfPoints => 33;

        public int GameWinPoints => 11;

        public int PlayerStartCards => 6;

        public int TrickCards => 2;

        public PlayerPosition RoundWinner { get; set; }

        public void EndRound()
        {
            OnEndRound?.Invoke();
        }

        public void RenderBoard()
        {
            OnRenderBoard?.Invoke();
        }

        public void ShowMessage(string message)
        {
            OnShowMessage?.Invoke(message);
        }
    }
}