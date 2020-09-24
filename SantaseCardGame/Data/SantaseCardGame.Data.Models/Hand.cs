namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Hand
    {
        public IEnumerable<Card> Cards { get; set; } = new List<Card>();
    }
}