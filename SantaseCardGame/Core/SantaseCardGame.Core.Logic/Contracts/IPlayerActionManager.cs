namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IPlayerActionManager
    {
        bool ShouldManage(PlayerAction playerAction);

        void Manage(PlayerAction playerAction, Player player);
    }
}