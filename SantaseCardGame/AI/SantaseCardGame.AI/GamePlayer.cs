namespace SantaseCardGame.AI
{
    using System;
    using System.Collections.Generic;
    
    using SantaseCardGame.AI.Contracts;
    using SantaseCardGame.AI.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class GamePlayer : IGamePlayer
    {
        private readonly IEnumerable<IPlayLogic> playLogics;

        public GamePlayer(IEnumerable<IPlayLogic> playLogics)
        {
            this.playLogics = playLogics;
        }

        public IEnumerable<PlayerAction> Play(Player player)
        {
            foreach (var playLogic in playLogics)
            {
                if (playLogic.ShouldPlay(player))
                {
                    return playLogic.Play(player);
                }
            }

            throw new InvalidOperationException("AI player could not choose action!");
        }
    }
}
