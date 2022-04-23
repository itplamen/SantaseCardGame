namespace SantaseCardGame.Core.Logic.Dealer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class CardsDealer : ICardsDealer
    {
        private readonly IGameRules gameRules;
        private readonly ICardsProvider cardsProvider;

        public CardsDealer(IGameRules gameRules, ICardsProvider cardsProvider)
        {
            this.gameRules = gameRules;
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
            for (int i = 1; i <= gameRules.RoundInitialCardsCount; i++)
            {
                Card card = deck.GetNextCard();
                AddCard(player, card);
            }
        }

        private void AddCard(Player player, Card card)
        {
            CardType type = GetMarriageType(card);
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

        private CardType GetMarriageType(Card card)
        {
            switch (card.Type)
            {
                case CardType.Queen:
                    return CardType.King;
                case CardType.King:
                    return CardType.Queen;
                default:
                    return CardType.None;
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
