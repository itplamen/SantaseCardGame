namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Player
    {
        public string Username { get; set; }

        public PlayerPosition Position { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();

        public List<Hand> Hands { get; set; } = new List<Hand>();
    }
}