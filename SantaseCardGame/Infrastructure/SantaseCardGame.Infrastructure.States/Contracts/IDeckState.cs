namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IDeckState
    {
        bool ShouldFollowSuit { get; set; }

        Card TrumpCard { get; set; }

        PlayerPosition ClosedBy { get; set; }

        int CardsLeft { get; set; }
    }
}
