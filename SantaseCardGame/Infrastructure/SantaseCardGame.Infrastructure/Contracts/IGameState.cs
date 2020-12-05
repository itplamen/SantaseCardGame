namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IGameState
    {
        event Action OnRenderBoard;

        event Action<string> OnShowMessage;

        Game Game { get; set; }

        int RoundWinPoints { get; }

        int RoundHalfPoints { get; }

        int GameWinPoints { get; }

        int PlayerStartCards { get; }

        int TrickCards { get; }

        void RenderBoard();

        void ShowMessage(string message);
    }
}