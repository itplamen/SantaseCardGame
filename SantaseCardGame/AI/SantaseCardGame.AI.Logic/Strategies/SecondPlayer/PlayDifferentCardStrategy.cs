namespace SantaseCardGame.AI.Logic.Strategies.SecondPlayer
{
    using System.Linq;

    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class PlayDifferentCardStrategy : IPlayerActionStrategy
    {
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IStorage<Game> gameStorage;

        public PlayDifferentCardStrategy(IGameState gameState, ITrickState trickState, IStorage<Game> gameStorage)
        {
            this.gameState = gameState;
            this.trickState = trickState;
            this.gameStorage = gameStorage;
        }

        public PlayerAction ChooseAction(Player player)
        {
            var opponentCard = trickState.Cards.First(x => x.Key != player.Position).Value;

            if (player.Cards.All(x => x.Suit != opponentCard.Suit))
            {
                Game game = gameStorage.Get(gameState.CurrentGameId);

                Card card = player.Cards
                    .Where(x => x.Suit != game.Deck.TrumpCard.Suit)
                    .OrderBy(x => x.Type)
                    .FirstOrDefault();

                return new PlayerAction(PlayerActionType.PlayCard, card);
            }

            return new PlayerAction(PlayerActionType.None);
        }
    }
}
