namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Deck
    {
        public Card TrumpCard { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();
    }
}