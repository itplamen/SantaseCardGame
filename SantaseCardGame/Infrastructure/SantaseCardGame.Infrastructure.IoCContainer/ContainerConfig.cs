namespace SantaseCardGame.Infrastructure.IoCContainer
{
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Packages;

    public static class ContainerConfig
    {
        public static void RegisterGameServices(this IServiceCollection services)
        {
            IPackage[] packages = new IPackage[]
            {
                new AIPackage(),
                new CorePackage(),
                new DataPackage(),
                new InfrastructurePackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(services);
            }
        }
    }
}