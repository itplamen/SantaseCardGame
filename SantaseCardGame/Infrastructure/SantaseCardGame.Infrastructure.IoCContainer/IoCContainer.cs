namespace SantaseCardGame.Infrastructure.IoCContainer
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.IoCPackages;

    public static class IoCContainer
    {
        public static IServiceCollection RegisterGameServices(this IServiceCollection services, IConfiguration configuration)
        {
            IPackage[] packages = new IPackage[]
            {
                new AIPackage(),
                new CorePackage(),
                new DataPackage(int.Parse(configuration["gameExpiration"])),
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
