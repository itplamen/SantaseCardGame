namespace SantaseCardGame.Infrastructure.States.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IDeckState
    {
        bool ShouldFollowSuit { get; set; }

        CardSuit TrumpCardSuit { get; set; }
    }
}
