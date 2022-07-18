namespace SantaseCardGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    public interface IStorage<TModel>
        where TModel : class
    {
        Task Add(TModel model);

        TModel Get(string id);

        Task<IEnumerable<TModel>> GetAll();

        Task Remove(string id);
    }
}
