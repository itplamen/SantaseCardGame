namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Data;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class DataPackage : IPackage
    {
        private int absoluteExpiration = 14400; // 60 * 60 * 4

        public void RegisterServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICardsProvider, CardsProvider>();
            services.AddSingleton<IInMemoryGameStorage, InMemoryGameStorage>(x => 
                new InMemoryGameStorage(x.GetRequiredService<IMemoryCache>(),
                absoluteExpiration));
        }
    }
}
