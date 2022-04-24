namespace SantaseCardGame.Infrastructure.States
{
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class DeckState : IDeckState
    {
        public bool ShouldFollowSuit { get; set; }

        public Card TrumpCard { get; set; }

        public PlayerPosition ClosedBy { get; set; }

        public int CardsLeft { get; set; }
    }
}