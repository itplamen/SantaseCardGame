namespace SantaseCardGame.Data.Models
{
    public class Card
    {
        public string Name { get; set; }

        public CardSuit Suit { get; set; }

        public CardType Type { get; set; }
    }
}
