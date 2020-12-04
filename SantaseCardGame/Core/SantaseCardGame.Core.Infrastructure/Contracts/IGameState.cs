namespace SantaseCardGame.Core.Infrastructure.Contracts
{
    using System;

    public interface IGameState
    {
        event Action OnRenderBoard;

        event Action<string> OnShowMessage;

        void RenderBoard();

        void ShowMessage(string message);
    }
}