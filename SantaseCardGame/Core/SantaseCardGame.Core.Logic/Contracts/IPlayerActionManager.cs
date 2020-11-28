namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionManager
    {
        bool ShouldManage(PlayerAction playerAction, Player player);

        void Manage(PlayerAction playerAction, Player player);
    }
}