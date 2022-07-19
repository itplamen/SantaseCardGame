namespace SantaseCardGame.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models.Contracts;

    public interface IStorage<TModel>
        where TModel : ISavable
    {
        Task Add(TModel model);

        TModel Get(string id);

        Task<IEnumerable<TModel>> GetAll();

        Task Remove(string id);
    }
}
