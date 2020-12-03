namespace SantaseCardGame.Core.Engine.IoCPackages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.AI;
    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Play.First;
    using SantaseCardGame.AI.Play.Second;
    using SantaseCardGame.AI.Strategies;
    using SantaseCardGame.Core.Engine.Contracts;
    using SantaseCardGame.Core.Infrastructure.Contracts;

    public sealed class AIPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IGamePlayer, GamePlayer>();
            services.AddTransient<IPlayLogic, AnnounceMarriage>();
            services.AddTransient<IPlayLogic, ChangeTrumpCard>();
            services.AddTransient<IPlayLogic, CloseDeck>();
            services.AddTransient<IPlayLogic, PlayCard>();
            services.AddTransient<IPlayLogic, PlayHigherCard>();
            services.AddTransient<IPlayLogic, PlayLowerCard>();
            services.AddTransient<IPlayLogic, PlayTrumpCard>();
            services.AddTransient<IPlayLogic, PlayDifferentCard>();

            RegisterStrategy(services, RegisterPlayFirstNotFollowingSuitStrategy);
        }

        private PlayFirstNotFollowingSuitStrategy RegisterPlayFirstNotFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            IEnumerable<Type> types = new List<Type>()
            {
                typeof(ChangeTrumpCard),
                typeof(CloseDeck),
                typeof(AnnounceMarriage),
                typeof(PlayCard)
            };

            IEnumerable<IPlayLogic> strategyLogics = playLogics.Where(x => types.Contains(x.GetType())).ToList();

            return new PlayFirstNotFollowingSuitStrategy(deckState, trickState, strategyLogics);
        }

        private void RegisterStrategy<TStrategy>(IServiceCollection services, Func<IDeckState, ITrickState, IEnumerable<IPlayLogic>, TStrategy> strategy)
            where TStrategy : BasePlayerActionStrategy
        {
            services.AddTransient<IPlayerActionStrategy, TStrategy>(x =>
                strategy(x.GetRequiredService<IDeckState>(), x.GetRequiredService<ITrickState>(), x.GetServices<IPlayLogic>()));
        }
    }
}