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
        private readonly ICardsProvider cardsProvider;

        public CardsDealer(IGameState gameState, ICardsProvider cardsProvider)
        {
            this.gameState = gameState;
            this.cardsProvider = cardsProvider;
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

        private void DealCards(Deck deck, Player player)
        {
            for (int i = 1; i <= gameState.PlayerStartCards; i++)
            {
                Card card = deck.GetNextCard();
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