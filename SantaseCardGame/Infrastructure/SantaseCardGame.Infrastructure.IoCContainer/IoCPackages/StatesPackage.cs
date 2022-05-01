namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.States;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public sealed class StatesPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IDeckState, DeckState>();
            services.AddSingleton<ITrickState, TrickState>();
        }
    }
}
