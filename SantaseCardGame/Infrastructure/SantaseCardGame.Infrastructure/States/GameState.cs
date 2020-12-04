namespace SantaseCardGame.Infrastructure.States
{
    using System;

    using SantaseCardGame.Infrastructure.Contracts;

    public class GameState : IGameState
    {
        public event Action OnRenderBoard;

        public event Action<string> OnShowMessage;

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