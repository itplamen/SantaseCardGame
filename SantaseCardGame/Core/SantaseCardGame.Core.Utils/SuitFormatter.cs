namespace SantaseCardGame.Core.Utils
{
    using System.Linq;

    using SantaseCardGame.Core.Utils.Contracts;
    using SantaseCardGame.Data.Models;

    public class SuitFormatter : ISuitFormatter
    {
        public string FormatSuit(CardSuit suit)
        {
            var memberInfo = suit.GetType()
                .GetMember(suit.ToString())
                .First();

            var attribute = memberInfo.CustomAttributes
                .First()
                .NamedArguments
                .First();

            return attribute.TypedValue
                .Value
                .ToString();
        }
    }
}