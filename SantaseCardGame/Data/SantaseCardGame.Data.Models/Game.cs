namespace SantaseCardGame.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models.Contracts;

    public class Game : ISavable
    {
        public string Id { get; set; }

        public bool IsSaved { get; set; }

        public DateTime Date { get; set; }

        public GameType Type { get; set; }

        public Deck Deck { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public ICollection<Round> Rounds { get; set; } = new List<Round>();
    }
}
