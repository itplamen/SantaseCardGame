namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayTrumpCardStrategy : IPlayerActionStrategy
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IGameState gameState;
        private readonly IStorage<Game> gameStorage;

        public PlayTrumpCardStrategy(IDeckState deckState, ITrickState trickState, IGameState gameState, IStorage<Game> gameStorage)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.gameState = gameState;
            this.gameStorage = gameStorage;
        }

        public PlayerAction ChooseAction(Player player)
        {
            Game game = gameStorage.Get(gameState.CurrentGameId);
            Card opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            if (ShouldPlayTrumpCardWhenFollowingSuit(player, opponentCard, game.Deck.TrumpCard) ||
                ShouldPlayTrumpCardWhenNotFollowingSuit(player, opponentCard, game.Deck.TrumpCard))
            {
                Card card = player.Cards
                    .Where(x => x.Suit == game.Deck.TrumpCard.Suit)
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }

        private bool ShouldPlayTrumpCardWhenFollowingSuit(Player player, Card opponentCard, Card trumpCard)
        {
            return deckState.ShouldFollowSuit &&
                opponentCard.Suit != trumpCard.Suit &&
                player.Cards.All(x => x.Suit != opponentCard.Suit);
        }

        private bool ShouldPlayTrumpCardWhenNotFollowingSuit(Player player, Card opponentCard, Card trumpCard)
        {
            return !deckState.ShouldFollowSuit &&
                opponentCard.Type >= CardType.Ten &&
                opponentCard.Suit != trumpCard.Suit &&
                !player.Cards.Any(x => x.Suit == opponentCard.Suit && x.Type > opponentCard.Type);
        }
    }
}
