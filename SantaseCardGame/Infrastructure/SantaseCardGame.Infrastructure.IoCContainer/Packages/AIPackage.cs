namespace SantaseCardGame.Infrastructure.IoCContainer.Packages
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
    using SantaseCardGame.Infrastructure.Contracts;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;

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
            RegisterStrategy(services, RegisterPlayFirstFollowingSuitStrategy);
            RegisterStrategy(services, RegisterPlaySecondNotFollowingSuitStrategy);
            RegisterStrategy(services, RegisterPlaySecondFollowingSuitStrategy);
        }

        private PlayFirstNotFollowingSuitStrategy RegisterPlayFirstNotFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            List<Type> types = new List<Type>()
            {
                typeof(ChangeTrumpCard),
                typeof(CloseDeck),
                typeof(AnnounceMarriage),
                typeof(PlayCard)
            };

            IEnumerable<IPlayLogic> strategyLogics = GetOrderedPlayLogics(types, playLogics);

            return new PlayFirstNotFollowingSuitStrategy(deckState, trickState, strategyLogics);
        }

        private PlayFirstFollowingSuitStrategy RegisterPlayFirstFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            List<Type> types = new List<Type>()
            {
                typeof(AnnounceMarriage),
                typeof(PlayCard)
            };

            IEnumerable<IPlayLogic> strategyLogics = GetOrderedPlayLogics(types, playLogics);

            return new PlayFirstFollowingSuitStrategy(deckState, trickState, strategyLogics);
        }

        private PlaySecondNotFollowingSuitStrategy RegisterPlaySecondNotFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            List<Type> types = new List<Type>()
            {
                typeof(PlayHigherCard),
                typeof(PlayLowerCard),
                typeof(PlayDifferentCard),
                typeof(PlayTrumpCard)
            };

            IEnumerable<IPlayLogic> strategyLogics = GetOrderedPlayLogics(types, playLogics);

            return new PlaySecondNotFollowingSuitStrategy(deckState, trickState, strategyLogics);
        }

        private PlaySecondFollowingSuitStrategy RegisterPlaySecondFollowingSuitStrategy(IDeckState deckState, ITrickState trickState, IEnumerable<IPlayLogic> playLogics)
        {
            List<Type> types = new List<Type>()
            {
                typeof(PlayHigherCard),
                typeof(PlayLowerCard),
                typeof(PlayTrumpCard),
                typeof(PlayDifferentCard)
            };

            IEnumerable<IPlayLogic> strategyLogics = GetOrderedPlayLogics(types, playLogics);

            return new PlaySecondFollowingSuitStrategy(deckState, trickState, strategyLogics);
        }

        private IEnumerable<IPlayLogic> GetOrderedPlayLogics(List<Type> playLogicTypes, IEnumerable<IPlayLogic> playLogics)
        {
            return playLogics.Where(x => playLogicTypes.Contains(x.GetType()))
                .OrderBy(x => playLogicTypes.IndexOf(x.GetType()))
                .ToList();
        }

        private void RegisterStrategy<TStrategy>(IServiceCollection services, Func<IDeckState, ITrickState, IEnumerable<IPlayLogic>, TStrategy> strategy)
            where TStrategy : BasePlayerActionStrategy
        {
            services.AddTransient<IPlayerActionStrategy, TStrategy>(x =>
                strategy(x.GetRequiredService<IDeckState>(), x.GetRequiredService<ITrickState>(), x.GetServices<IPlayLogic>()));
        }
    }
}