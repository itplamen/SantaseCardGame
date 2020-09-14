namespace SantaseCardGame.Web.Client.Infrastructure
{
    using System;

    public class GameState
    {
        public bool IsClosed { get; set; }

        public event Action OnCloseDeck;

        public void CloseDeck()
        {
            if (!IsClosed)
            {
                OnCloseDeck?.Invoke();
            }
        }
    }
}