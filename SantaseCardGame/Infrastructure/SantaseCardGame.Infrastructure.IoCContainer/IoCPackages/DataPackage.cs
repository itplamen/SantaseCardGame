namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;

    using SantaseCardGame.Data;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Models;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICardsProvider, CardsProvider>();

            services.AddSingleton(x => 
                new LocalGameStorage(
                    x.GetRequiredService<IJSRuntime>(),
                    x.GetRequiredService<IConfiguration>()));

            services.AddSingleton<StateStorage>();
            services.AddSingleton<IStorage<State>, StateStorage>(x => 
                new StateStorage(
                    x.GetRequiredService<IJSRuntime>(),
                    x.GetRequiredService<IConfiguration>()));

            services.AddSingleton<IStorage<Game>, InMemoryGameStorage>(x => 
                new InMemoryGameStorage(
                    x.GetRequiredService<LocalGameStorage>(),
                    x.GetRequiredService<IMemoryCache>(),
                    x.GetRequiredService<IConfiguration>()));
        }
    }
}
