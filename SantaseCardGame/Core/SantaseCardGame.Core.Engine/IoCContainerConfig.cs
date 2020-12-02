namespace SantaseCardGame.Core.Engine
{
    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Engine.IoCPackages;

    public static class IoCContainerConfig
    {
        public static void RegisterGameServices(this IServiceCollection services)
        {
            IPackage[] packages = new IPackage[]
            {
                new AIPackage(),
                new CorePackage(),
                new DataPackage()
            };

            foreach (var package in packages)
            {
                package.RegisterServices(services);
            }
        }
    }
}