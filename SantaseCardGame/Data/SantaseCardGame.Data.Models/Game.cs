namespace SantaseCardGame.Data.Models
{
    using System.Collections.Generic;

    public class Game
    {
        public Player FirstPlayer { get; set; }

        public Player SecondPlayer { get; set; }

        public Deck Deck { get; set; }

        public List<Round> Rounds { get; set; } = new List<Round>();
    }
}