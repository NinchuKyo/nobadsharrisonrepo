using System.ComponentModel.DataAnnotations;

namespace AOERandomizer.ViewModel.Enums
{
    /// <summary>
    /// Enum for the different types of editable-data this application can access.
    /// </summary>
    public enum EPageName
    {
        [Display(Name = "Teams")]
        Teams,

        [Display(Name = "Civs")]
        Civs,

        [Display(Name = "Maps")]
        Maps,

        [Display(Name = "Coin Flip")]
        CoinFlip
    }
}