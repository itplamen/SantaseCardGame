namespace SantaseCardGame.Core.Logic.Managers
{
    using System.Collections.Generic;
    using System.Linq;

    using SantaseCardGame.Core.Infrastructure.Contracts;
    using SantaseCardGame.Core.Logic.Contracts;
    using SantaseCardGame.Data.Models;

    public class PlayerManager : IPlayerManager
    {
        private readonly IDeckState deckState;
        private readonly ITrickState trickState;
        private readonly IAnnounceCardProvider announceCardProvider;
        private readonly IPlayerActionValidator playerActionValidator;

        public PlayerManager(IDeckState deckState, ITrickState trickState, IAnnounceCardProvider announceCardProvider, IPlayerActionValidator playerActionValidator)
        {
            this.deckState = deckState;
            this.trickState = trickState;
            this.announceCardProvider = announceCardProvider;
            this.playerActionValidator = playerActionValidator;
        }

        public bool ChangeTrumpCard(Player player, Card trumpCard)
        {
            if (playerActionValidator.CanChangeTrump(player))
            {
                Card nineOfTrumpsCard = player.Cards.FirstOrDefault(x => x.Type == CardType.Nine && x.Suit == trumpCard.Suit);

                if (nineOfTrumpsCard != null)
                {
                    int nineOfTrumpsIndex = player.Cards.FindIndex(x => x.Name == nineOfTrumpsCard.Name);
                    player.Cards[nineOfTrumpsIndex] = trumpCard;
                    deckState.ExchangeTrumpCardForNineOfTrumps(nineOfTrumpsCard);

                    return true;
                }
            }

            return false;
        }

        public bool CloseDeck(Player player)
        {
            if (playerActionValidator.CanCloseDeck(player))
            {
                deckState.IsClosed = true;
            }

            return deckState.IsClosed;
        }

        public Announce PlayCard(Player player, Card card)
        {
            Card opponentTrickCard = trickState.Cards.FirstOrDefault(x => x.Key != player.Position).Value;

            if (player.Position == trickState.PlayerTurn)
            {
                if (opponentTrickCard != null && deckState.ShouldFollowSuit)
                {
                    IEnumerable<Card> sameSuitCards = player.Cards.Where(x => x.Suit == opponentTrickCard.Suit);

                    if (sameSuitCards.Any(x => x.Type > opponentTrickCard.Type) && card.Type < opponentTrickCard.Type)
                    {
                        return Announce.None;
                    }

                    if (sameSuitCards.Any() && card.Suit != opponentTrickCard.Suit)
                    {
                        return Announce.None;
                    }

                    if (!sameSuitCards.Any() && player.Cards.Any(x => x.Suit == trickState.TrumpCardSuit) && card.Suit != trickState.TrumpCardSuit)
                    {
                        return Announce.None;
                    }
                }

                Announce announce = announceCardProvider.GetAnnounce(player, card).Announce;

                if (announce != Announce.None)
                {
                    player.Announcements.Add(card.Suit, announce);
                }

                player.Cards.Remove(card);
                trickState.AddCard(card, player.Position);

                return announce;
            }

            return Announce.None;
        }
    }
}
