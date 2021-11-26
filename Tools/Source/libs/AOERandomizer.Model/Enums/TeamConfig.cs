using System.ComponentModel.DataAnnotations;

namespace AOERandomizer.Model.Enums
{
    public enum TeamConfig
    {
        [Display(Name = "2v2")]
        TwoVsTwo,

        [Display(Name = "2v2v2")]
        TwoVsTwoVsTwo,

        [Display(Name = "2v2v2v2")]
        TwoVsTwoVsTwoVsTwo,

        [Display(Name = "3v3")]
        ThreeVsThree,

        [Display(Name = "4v4")]
        FourVsFour,
    }
}