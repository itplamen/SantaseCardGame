namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IGameState
    {
        event Action OnEndRound;

        event Action OnRenderBoard;

        event Action<string> OnShowMessage;

        int RoundWinPoints { get; }

        int RoundHalfPoints { get; }

        int GameWinPoints { get; }

        int PlayerStartCards { get; }

        int TrickCards { get; }

        PlayerPosition RoundWinner { get; set; }

        void EndRound();

        void RenderBoard();

        void ShowMessage(string message);
    }
}