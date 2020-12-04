namespace SantaseCardGame.Infrastructure.IoCContainer.Packages
{
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Providers;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICardsProvider, CardsProvider>();
        }
    }
}