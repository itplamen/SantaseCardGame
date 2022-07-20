namespace SantaseCardGame.Data.Models.Contracts
{
    using System;

    public interface ISavable
    {
        public string Id { get; set; }

        public bool IsSaved { get; set; }

        public DateTime Date { get; set; }
    }
}
