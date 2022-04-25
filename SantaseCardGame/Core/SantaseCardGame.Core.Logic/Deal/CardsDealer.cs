namespace SantaseCardGame.Core.Logic.Deal
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
        private readonly IAnnouncementChecker announcementChecker;

        public CardsDealer(IGameRules gameRules, ICardsProvider cardsProvider, IAnnouncementChecker announcementChecker)
        {
            this.gameRules = gameRules;
            this.cardsProvider = cardsProvider;
            this.announcementChecker = announcementChecker;
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
            // Add marriage cards close to each other
            CardType searchType = announcementChecker.MarriageCardTypeToSearch(card);
            int index = player.GetCardPosition(searchType, card.Suit);

            player.AddCard(card, index);
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
