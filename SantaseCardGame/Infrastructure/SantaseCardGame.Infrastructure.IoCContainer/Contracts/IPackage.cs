namespace SantaseCardGame.Infrastructure.IoCContainer.Contracts
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IPackage
    {
        void RegisterServices(IServiceCollection services);
    }
}
