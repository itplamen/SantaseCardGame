namespace SantaseCardGame.Core.Engine
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class GameEngine : IGameEngine
    {
        private readonly IGamePlayer gamePlayer;
        private readonly ITrickState trickState;
        private readonly IDeckState deckState;
        private readonly ITrickWinner trickWinner;
        private readonly ICardsDealer cardsDealer;
        private readonly ICardsProvider cardsProvider;
        private readonly ICardsShuffler cardsShuffler;

        public GameEngine(IGamePlayer gamePlayer, ITrickState trickState, IDeckState deckState, ITrickWinner trickWinner, ICardsDealer cardsDealer, ICardsProvider cardsProvider, ICardsShuffler cardsShuffler)
        {
            this.gamePlayer = gamePlayer;
            this.trickState = trickState;
            this.deckState = deckState;
            this.trickWinner = trickWinner;
            this.cardsDealer = cardsDealer;
            this.cardsProvider = cardsProvider;
            this.cardsShuffler = cardsShuffler;
        }

        public Game StartGame(string username)
        {
            IEnumerable<Card> cards = cardsProvider.Get();
            Deck deck = cardsShuffler.Shuffle(cards);

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

            cardsDealer.Deal(deck, firstPlayer, secondPlayer);

            return new Game()
            {
                Deck = deck,
                FirstPlayer = firstPlayer,
                SecondPlayer = secondPlayer
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

            if (winnerPosition == game.FirstPlayer.Position)
            {
                game.FirstPlayer.Hands.Add(hand);
            }
            else
            {
                game.SecondPlayer.Hands.Add(hand);
            }

            await Task.Delay(1500);

            trickState.Clear();
            trickState.PlayerTurn = winnerPosition;

            DrawCards(winnerPosition, game);
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

            if (game.FirstPlayer.Position == winnerPosition)
            {
                game.FirstPlayer.Cards.Add(firstCard);
                game.SecondPlayer.Cards.Add(secondCard);
            }
            else
            {
                game.SecondPlayer.Cards.Add(firstCard);
                game.FirstPlayer.Cards.Add(secondCard);
            }

            if (!game.Deck.Cards.Any())
            {
                deckState.ShouldFollowSuit = true;
            }
        }
    }
}