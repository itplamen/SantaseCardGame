namespace SantaseCardGame.Core.Logic.Deal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public class CardsDealer : ICardsDealer
    {
        private readonly IGameState gameState;
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly ICardsProvider cardsProvider;
        private readonly IAnnouncementChecker announcementChecker;

        public CardsDealer(IGameState gameState, IDeckState deckState, ITrickState trickState, ICardsProvider cardsProvider, IAnnouncementChecker announcementChecker)
        {
            this.gameState = gameState;
            this.deckState = deckState;
            this.trickState = trickState;
            this.cardsProvider = cardsProvider;
            this.announcementChecker = announcementChecker;
        }

        public Deck Deal(Player firstPlayer, Player secondPlayer)
        {
            IEnumerable<Card> cards = cardsProvider.Get();
            Deck deck = Shuffle(cards);

            DealCards(deck, firstPlayer);
            DealCards(deck, secondPlayer);

            return deck;
        }

        public void DrawCards(Deck deck, IEnumerable<Player> players)
        {
            Card firstCard = deck.GetNextCard();
            Card secondCard = deck.GetNextCard();

            Player winnerPlayer = players.First(x => x.Position == trickState.PlayerTurn);
            AddCard(winnerPlayer, firstCard);

            Player loserPlayer = players.First(x => x.Position != trickState.PlayerTurn);
            AddCard(loserPlayer, secondCard);

            deckState.CardsLeft = deck.Cards.Count();
            deckState.ShouldFollowSuit = !deck.Cards.Any();
        }

        private void DealCards(Deck deck, Player player)
        {
            for (int i = 1; i <= gameState.RoundInitialCardsCount; i++)
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
            if (cards == null || !cards.Any())
            {
                throw new ArgumentNullException("There are no cards to shuffle!");
            }

            Deck deck = new Deck();
            List<Card> list = cards.ToList();

            for (var i = 0; i < list.Count; i++)
            {
                int index = i + RandomGenerator.Next(0, list.Count - i);

                Card temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }

            list.ForEach(x => deck.AddCard(x));

            return deck;
        }
    }
}
