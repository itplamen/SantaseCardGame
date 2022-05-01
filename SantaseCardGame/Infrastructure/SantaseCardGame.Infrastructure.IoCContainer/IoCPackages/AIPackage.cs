namespace SantaseCardGame.Infrastructure.IoCContainer.IoCPackages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using SantaseCardGame.AI;
    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.AI.Logic.PlayLogics;
    using SantaseCardGame.AI.Logic.PlayLogics.FirstPlayer;
    using SantaseCardGame.AI.Logic.PlayLogics.SecondPlayer;
    using SantaseCardGame.AI.Logic.Strategies.FirstPlayer;
    using SantaseCardGame.AI.Logic.Strategies.SecondPlayer;
    using SantaseCardGame.Infrastructure.IoCContainer.Contracts;
    using SantaseCardGame.Infrastructure.States.Contracts;

    public sealed class AIPackage : IPackage
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IGamePlayer, GamePlayer>();

            services.AddSingleton<IPlayerActionStrategy, AnnounceMarriageStrategy>();
            services.AddSingleton<IPlayerActionStrategy, ChangeTrumpCardStrategy>();
            services.AddSingleton<IPlayerActionStrategy, CloseDeckStrategy>();
            services.AddSingleton<IPlayerActionStrategy, PlayCardStrategy>();
            services.AddSingleton<IPlayerActionStrategy, PlayDifferentCardStrategy>();
            services.AddSingleton<IPlayerActionStrategy, PlayHigherCardStrategy>();
            services.AddSingleton<IPlayerActionStrategy, PlayLowerCardStrategy>();
            services.AddSingleton<IPlayerActionStrategy, PlayTrumpCardStrategy>();

            RegisterPlayingLogic(services, RegisterPlayFirstFollowingSuitLogic);
            RegisterPlayingLogic(services, RegisterPlayFirstNotFollowingSuitLogic);
            RegisterPlayingLogic(services, RegisterPlaySecondFollowingSuitLogic);
            RegisterPlayingLogic(services, RegisterPlaySecondNotFollowingSuitLogic);
        }

        private PlayFirstFollowingSuitLogic RegisterPlayFirstFollowingSuitLogic(ITrickState trickState, IDeckState deckState, IEnumerable<IPlayerActionStrategy> strategies)
        {
            List<Type> types = new List<Type>()
            {
                typeof(AnnounceMarriageStrategy),
                typeof(PlayCardStrategy)
            };

            IEnumerable<IPlayerActionStrategy> orderedStrategies = GetOrderedStrategies(types, strategies);

            return new PlayFirstFollowingSuitLogic(trickState, deckState, orderedStrategies);
        }

        private PlayFirstNotFollowingSuitLogic RegisterPlayFirstNotFollowingSuitLogic(ITrickState trickState, IDeckState deckState, IEnumerable<IPlayerActionStrategy> strategies)
        {
            List<Type> types = new List<Type>()
            {
                typeof(ChangeTrumpCardStrategy),
                typeof(CloseDeckStrategy),
                typeof(AnnounceMarriageStrategy),
                typeof(PlayCardStrategy)
            };

            IEnumerable<IPlayerActionStrategy> orderedStrategies = GetOrderedStrategies(types, strategies);

            return new PlayFirstNotFollowingSuitLogic(trickState, deckState, orderedStrategies);
        }

        private PlaySecondFollowingSuitLogic RegisterPlaySecondFollowingSuitLogic(ITrickState trickState, IDeckState deckState, IEnumerable<IPlayerActionStrategy> strategies)
        {
            List<Type> types = new List<Type>()
            {
                typeof(PlayHigherCardStrategy),
                typeof(PlayLowerCardStrategy),
                typeof(PlayTrumpCardStrategy),
                typeof(PlayDifferentCardStrategy)
            };

            IEnumerable<IPlayerActionStrategy> orderedStrategies = GetOrderedStrategies(types, strategies);

            return new PlaySecondFollowingSuitLogic(trickState, deckState, orderedStrategies);
        }

        private PlaySecondNotFollowingSuitLogic RegisterPlaySecondNotFollowingSuitLogic(ITrickState trickState, IDeckState deckState, IEnumerable<IPlayerActionStrategy> strategies)
        {
            List<Type> types = new List<Type>()
            {
                typeof(PlayHigherCardStrategy),
                typeof(PlayLowerCardStrategy),
                typeof(PlayDifferentCardStrategy),
                typeof(PlayTrumpCardStrategy)
            };

            IEnumerable<IPlayerActionStrategy> orderedStrategies = GetOrderedStrategies(types, strategies);

            return new PlaySecondNotFollowingSuitLogic(trickState, deckState, orderedStrategies);
        }

        private IEnumerable<IPlayerActionStrategy> GetOrderedStrategies(List<Type> playLogicTypes, IEnumerable<IPlayerActionStrategy> strategies)
        {
            return strategies.Where(x => playLogicTypes.Contains(x.GetType()))
                .OrderBy(x => playLogicTypes.IndexOf(x.GetType()))
                .ToList();
        }

        private void RegisterPlayingLogic<TPlayLogic>(IServiceCollection services, Func<ITrickState, IDeckState, IEnumerable<IPlayerActionStrategy>, TPlayLogic> playingLogic)
            where TPlayLogic : BasePlayLogic
        {
            services.AddSingleton<IPlayLogic, TPlayLogic>(x => 
                playingLogic(x.GetRequiredService<ITrickState>(), x.GetRequiredService<IDeckState>(), x.GetServices<IPlayerActionStrategy>()));
        }
    }
}
