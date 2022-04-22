namespace SantaseCardGame.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;

    public class CardsProvider : ICardsProvider
    {
        private readonly ICollection<Card> cards = new List<Card>();

        public IEnumerable<Card> Get()
        {
            if (!cards.Any())
            {
                IEnumerable<string> cardNames = GenerateNames();

                foreach (var name in cardNames)
                {
                    (CardType type, CardSuit suit) = GetDetails(name);

                    if (!cards.Any(x => x.Name == name))
                    {
                        var card = new Card()
                        {
                            Type = type,
                            Suit = suit,
                            Name = name
                        };

                        cards.Add(card);
                    }
                }
            }

            return cards;
        }

        private IEnumerable<string> GenerateNames()
        {
            var names = new List<string>();
            var cardTypes = ((CardType[])Enum.GetValues(typeof(CardType)))
                .Where(x => x != CardType.None);

            foreach (var cardType in cardTypes)
            {
                foreach (var cardSuit in (CardSuit[])Enum.GetValues(typeof(CardSuit)))
                {
                    names.Add($"{cardType}{cardSuit}");
                }
            }

            return names;
        }

        private (CardType, CardSuit) GetDetails(string name)
        {
            string[] details = Regex.Replace(name, "[a-z][A-Z]", x => x.Value[0] + " " + x.Value[1]).Split(" ");
            CardType type = Enum.Parse<CardType>(details.First());
            CardSuit suit = Enum.Parse<CardSuit>(details.Last());

            return (type, suit);
        }
    }
}
