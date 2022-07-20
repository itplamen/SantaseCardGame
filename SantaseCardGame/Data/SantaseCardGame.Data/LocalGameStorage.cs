namespace SantaseCardGame.Data
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.JSInterop;
    
    using SantaseCardGame.Data.Models;
    
    public class LocalGameStorage : BaseStorage<Game>
    {
        public LocalGameStorage(IJSRuntime jsRuntime, IConfiguration configuration)
            : base(jsRuntime, configuration, "gameKey")
        {
        }
    }
}
