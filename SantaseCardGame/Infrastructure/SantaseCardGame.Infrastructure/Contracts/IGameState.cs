namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IGameState
    {
        event Action OnEndRound;

        event Action OnRenderBoard;

        event Action<PlayerPosition, string> OnShowMessage;

        int RoundWinPoints { get; }

        int RoundHalfPoints { get; }

        int GameWinPoints { get; }

        int PlayerStartCards { get; }

        int TrickCards { get; }

        int SimulateDelay { get; }

        GameType GameType { get; set; }

        PlayerPosition RoundWinner { get; set; }

        void EndRound();

        void RenderBoard();

        void ShowMessage(PlayerPosition possition, string message);
    }
}