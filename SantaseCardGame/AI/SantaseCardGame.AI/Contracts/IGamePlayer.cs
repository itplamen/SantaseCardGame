namespace SantaseCardGame.AI.Contracts
{
    using System.Threading.Tasks;

    using SantaseCardGame.Data.Models;

    public interface IGamePlayer
    {
        Task<PlayerAction> Play(Player player);
    }
}
