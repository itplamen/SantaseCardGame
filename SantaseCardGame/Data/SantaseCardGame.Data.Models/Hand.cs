namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Hand
    {
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}