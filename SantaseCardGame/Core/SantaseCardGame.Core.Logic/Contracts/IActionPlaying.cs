namespace SantaseCardGame.Core.Logic.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface IActionPlaying
    {
        PlayerActionResult Play(PlayerAction playerAction, Player player);
    }
}
