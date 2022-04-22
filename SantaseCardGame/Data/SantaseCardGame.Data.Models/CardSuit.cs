namespace SantaseCardGame.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public enum CardSuit
    {
        [Display(Name = "♣")]
        Club,

        [Display(Name = "♦")]
        Diamond,

        [Display(Name = "♥")]
        Heart,

        [Display(Name = "♠")]
        Spade
    }
}
