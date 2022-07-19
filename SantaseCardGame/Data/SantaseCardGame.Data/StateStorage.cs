namespace SantaseCardGame.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Models;
    
    public class StateStorage : BaseStorage<State>
    {
        public StateStorage(IJSRuntime jsRuntime, IConfiguration configuration)
            : base(jsRuntime, configuration, "stateKey")
        {
        }

        public override async Task Remove(string id)
        {
            var states = await GetAll();
            var state = states.FirstOrDefault(x => x.GameId == id);

            if (state != null)
            {
                states.ToList().Remove(state);
            }

            await Save(states);
        }
    }
}
