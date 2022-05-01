namespace SantaseCardGame.Infrastructure.IoCContainer
{
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.IoCPackages;

    public static class IoCContainer
    {
        public static IServiceCollection RegisterGameServices(this IServiceCollection services)
        {
            IPackage[] packages = new IPackage[]
            {
                new AIPackage(),
                new CorePackage(),
                new DataPackage(),
                new StatesPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(services);
            }

            return services;
        }
    }
}
