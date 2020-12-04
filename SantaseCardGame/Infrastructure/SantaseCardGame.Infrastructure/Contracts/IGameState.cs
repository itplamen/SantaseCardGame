namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

    using SantaseCardGame.Data.Models;

    public interface IGameState
    {
        event Action OnRenderBoard;

        event Action<string> OnShowMessage;

        Game Game { get; set; }

        void RenderBoard();

        void ShowMessage(string message);
    }
}