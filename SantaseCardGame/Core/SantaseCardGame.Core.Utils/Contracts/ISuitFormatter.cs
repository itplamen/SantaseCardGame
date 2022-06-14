namespace SantaseCardGame.Core.Utils.Contracts
{
    using SantaseCardGame.Data.Models;

    public interface ISuitFormatter
    {
        string FormatSuit(CardSuit suit);
    }
}