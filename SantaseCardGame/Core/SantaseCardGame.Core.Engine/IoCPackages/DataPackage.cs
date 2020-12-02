namespace SantaseCardGame.Core.Engine.IoCPackages
{
    using Microsoft.Extensions.DependencyInjection;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Data.Contracts;
    using SantaseCardGame.Data.Providers;

    public sealed class DataPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<ICardsProvider, CardsProvider>();
        }
    }
}