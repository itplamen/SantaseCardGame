namespace SantaseCardGame.Data.Models
{
    using System;
    using System.Collections.Generic;

    using SantaseCardGame.Data.Models.Contracts;

    public class State : ISavable
    {
        public string Id { get; set; }

        public bool IsSaved { get; set; }

        public DateTime Date { get; set; }

        public bool ShouldFollowSuit { get; set; }

        public PlayerPosition PlayerTurn { get; set; }

        public IEnumerable<KeyValuePair<PlayerPosition, Card>> TrickCards { get; set; } = new Dictionary<PlayerPosition, Card>();
    }
}
