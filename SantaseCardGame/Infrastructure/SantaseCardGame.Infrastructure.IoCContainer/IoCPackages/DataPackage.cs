namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Data;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class DataPackage : IPackage
    {
        private int absoluteExpiration;

        public DataPackage(int absoluteExpiration)
        {
            this.absoluteExpiration = absoluteExpiration;
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICardsProvider, CardsProvider>();

            services.AddSingleton<LocalGameStorage>();
            services.AddSingleton<IGameStorage, InMemoryGameStorage>(x => 
                new InMemoryGameStorage(
                    x.GetRequiredService<LocalGameStorage>(),
                    x.GetRequiredService<IMemoryCache>(),
                    absoluteExpiration));
        }
    }
}
