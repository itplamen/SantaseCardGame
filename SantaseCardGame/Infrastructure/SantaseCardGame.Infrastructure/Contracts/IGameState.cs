namespace SantaseCardGame.Infrastructure.Contracts
{
    using System;

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

        void EndRound();

        void RenderBoard();

        void ShowMessage(string message);
    }
}