namespace SantaseCardGame.Core.Logic.Shuffle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    // Fisher–Yates algorithm
    public class CardsShuffler : ICardsShuffler
    {
        public Deck Shuffle(IEnumerable<Card> cards)
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