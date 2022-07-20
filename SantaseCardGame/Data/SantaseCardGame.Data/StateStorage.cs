namespace SantaseCardGame.Data
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Models;
    
    public class StateStorage : BaseStorage<State>
    {
        public StateStorage(IJSRuntime jsRuntime, IConfiguration configuration)
            : base(jsRuntime, configuration, "stateKey")
        {
        }
    }
}
