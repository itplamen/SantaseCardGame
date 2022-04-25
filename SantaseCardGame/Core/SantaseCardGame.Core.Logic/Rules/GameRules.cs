namespace SantaseCardGame.Core.Logic.Rules
{
    using SantaseCardGame.Core.Logic.Contracts;

    public class GameRules : IGameRules
    {
        public int RoundInitialCardsCount => 6;

        public int DeckMinCardsBeforeClosing => 4;

        public int MarriageCardsCount => 2;

        public int TrickCardsCount => 2;

        public int GameWinPoints => 11;

        public int RoundWinPoints => 66;

        public int RoundHalfPoints => 33;

        public int PlayerWinMaxRoundPoints => 3;

        public int PlayerWinHalfRoundPoints => 2;

        public int PlayerWinMinRoundPoints => 1;

        public int LastTrickWinnerBonusPoints => 10;
    }
}
