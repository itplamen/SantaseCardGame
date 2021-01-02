namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class RoundManager : IRoundManager
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly ITrickWinner trickWinner;
        private readonly ICardsDealer cardsDealer;
        private readonly IEnumerable<IRoundWinner> roundWinners;

        public RoundManager(IGameState gameState, IDeckState deckState, ITrickState trickState, ITrickWinner trickWinner, ICardsDealer cardsDealer, IEnumerable<IRoundWinner> roundWinners)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
            this.trickWinner = trickWinner;
            this.cardsDealer = cardsDealer;
            this.roundWinners = roundWinners;
        }

        public Deck StartRound(IEnumerable<Player> players)
        {
            foreach (var player in players)
            {
                player.Cards.Clear();
                player.Hands.Clear();
                player.Announcements.Clear();
            }

            Deck deck = cardsDealer.Deal(players.First(), players.Last());

            deckState.ShouldFollowSuit = false;
            deckState.TrumpCard = deck.TrumpCard;
            deckState.CardsLeft = deck.Cards.Count;
            deckState.ClosedBy = PlayerPosition.NoOne;
            trickState.PlayerTurn = PlayerPosition.First; // TODO:

            return deck;
        }

        public void EndRound(Round round, Game game)
        {
            game.Rounds.Add(round);
            gameState.EndRound();
        }

        public Round GetRoundWinner(IEnumerable<Player> players)
        {
            foreach (var roundWinner in roundWinners)
            {
                Round round = roundWinner.GetWinner(players);

                if (round.WinnerPosition != PlayerPosition.NoOne)
                {
                    return round;
                }
            }

            return new Round();
        }

        public PlayerPosition PlayTrick(Game game)
        {
            PlayerPosition winnerPosition = trickWinner.GetWinner(trickState.Cards, game.Deck.TrumpCard.Suit);
            Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);

            Hand hand = new Hand() { Cards = trickState.Cards.Select(x => x.Value) };
            winnerPlayer.Hands.Add(hand);

            trickState.PlayerTurn = winnerPosition;

            return winnerPosition;
        }
    }
}