namespace SantaseCardGame.Core.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class GameEngine : IGameEngine
    {
        private readonly IGamePlayer gamePlayer;
        private readonly IGameState gameState;
        private readonly ITrickState trickState;
        private readonly IDeckState deckState;
        private readonly ITrickWinner trickWinner;
        private readonly ICardsDealer cardsDealer;

        public GameEngine(IGamePlayer gamePlayer, IGameState gameState, ITrickState trickState, IDeckState deckState, ITrickWinner trickWinner, ICardsDealer cardsDealer)
        {
            this.gamePlayer = gamePlayer;
            this.gameState = gameState;
            this.trickState = trickState;
            this.deckState = deckState;
            this.trickWinner = trickWinner;
            this.cardsDealer = cardsDealer;
        }

        public Game StartGame(string username)
        {
            var firstPlayer = new Player()
            {
                Username = "Bot",
                Position = PlayerPosition.First
            };

            var secondPlayer = new Player()
            {
                Username = username,
                Position = PlayerPosition.Second
            };

            Deck deck = cardsDealer.Deal(firstPlayer, secondPlayer);

            return new Game()
            {
                Deck = deck,
                Players = new List<Player>() { firstPlayer, secondPlayer }
            };
        }

        public async void AIPlay(Player player)
        {
            await Task.Delay(1500);

            gamePlayer.Play(player);
        }

        public async void PlayTrick(Game game)
        {
            Hand hand = new Hand()
            {
                Cards = trickState.Cards.Select(x => x.Value)
            };

            PlayerPosition winnerPosition = trickWinner.GetWinner(trickState.Cards, game.Deck.TrumpCard.Suit);
            Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);
            winnerPlayer.Hands.Add(hand);
            
            await Task.Delay(1500);

            trickState.Clear();
            trickState.PlayerTurn = winnerPosition;

            DrawCards(winnerPosition, game);

            gameState.RenderBoard();

        }

        private void DrawCards(PlayerPosition winnerPosition, Game game)
        {
            if (deckState.ClosedBy != PlayerPosition.NoOne || !game.Deck.Cards.Any())
            {
                return;
            }

            Card firstCard = game.Deck.GetNextCard();
            Card secondCard = game.Deck.GetNextCard();

            deckState.CardsLeft = game.Deck.Cards.Count;
            
            Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);
            winnerPlayer.Cards.Add(firstCard);

            Player loserPlayer = game.Players.First(x => x.Position != winnerPosition);
            loserPlayer.Cards.Add(secondCard);

            if (!game.Deck.Cards.Any())
            {
                deckState.ShouldFollowSuit = true;
            }
        }
    }
}