namespace SantaseCardGame.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IDataStorage<TValue>
        where TValue : class
    {
        Task Set(string key, TValue value);

        Task<TValue> Get(string key);
    }
}
