namespace SantaseCardGame.Core.Logic.Deal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.Contracts;

    public class CardsDealer : ICardsDealer
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ICardsProvider cardsProvider;
        private readonly IAnnounceCardProvider announceCardProvider;

        public CardsDealer(IGameState gameState, IDeckState deckState, ICardsProvider cardsProvider, IAnnounceCardProvider announceCardProvider)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.cardsProvider = cardsProvider;
            this.announceCardProvider = announceCardProvider;
        }

        public Deck Deal(Player firstPlayer, Player secondPlayer)
        {
            IEnumerable<Card> cards = cardsProvider.Get();
            Deck deck = Shuffle(cards);

            DealCards(deck, firstPlayer);
            DealCards(deck, secondPlayer);

            deck.TrumpCard = deck.Cards.Last();

            return deck;
        }

        public void DrawCards(PlayerPosition winnerPosition, Game game)
        {
            if (deckState.ClosedBy == PlayerPosition.NoOne && game.Deck.Cards.Any())
            {
                Card firstCard = game.Deck.GetNextCard();
                Card secondCard = game.Deck.GetNextCard();

                Player winnerPlayer = game.Players.First(x => x.Position == winnerPosition);
                AddCard(winnerPlayer, firstCard);

                Player loserPlayer = game.Players.First(x => x.Position != winnerPosition);
                AddCard(loserPlayer, secondCard);

                deckState.CardsLeft = game.Deck.Cards.Count;
                deckState.ShouldFollowSuit = !game.Deck.Cards.Any();

                gameState.RenderBoard();
            }
        }

        private void DealCards(Deck deck, Player player)
        {
            for (int i = 1; i <= gameState.PlayerStartCards; i++)
            {
                Card card = deck.GetNextCard();
                AddCard(player, card);
            }
        }

        private void AddCard(Player player, Card card)
        {
            CardType type = announceCardProvider.AnnounceCardTypeToSearch(card);
            int index = player.Cards.FindIndex(x => x.Type == type && x.Suit == card.Suit);

            if (index >= 0)
            {
                player.Cards.Insert(index, card);
            }
            else
            {
                player.Cards.Add(card);
            }
        }

        // Fisher–Yates algorithm
        private Deck Shuffle(IEnumerable<Card> cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException("There are no cards to shuffle!");
            }

            List<Card> list = cards.ToList();

            for (var i = 0; i < list.Count; i++)
            {
                int index = i + RandomGenerator.Next(0, list.Count - i);

                Card temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }

            return new Deck()
            {
                Cards = list
            };
        }
    }
}