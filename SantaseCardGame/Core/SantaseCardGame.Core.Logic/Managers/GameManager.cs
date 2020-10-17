namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    
    public class GameManager : IGameManager
    {
        private readonly ICardsDealer cardsDealer;
        private readonly ICardsProvider cardsProvider;
        private readonly ICardsShuffler cardsShuffler;

        public GameManager(ICardsDealer cardsDealer, ICardsProvider cardsProvider, ICardsShuffler cardsShuffler)
        {
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
    }
}