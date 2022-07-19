namespace SantaseCardGame.Data.Models.Contracts
{
    public interface ISavable
    {
        public string Id { get; set; }

        public bool IsSaved { get; set; }
    }
}
