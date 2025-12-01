using System.ComponentModel.DataAnnotations;

namespace StrategyBased;

public enum StrategyType
{
    None = 0,

    NakedSingles /*...*/ = 01,
    HiddenSingles /*..*/ = 02,

    PointingDigits /*.*/ = 03,

    [Display(Name = "h2")]
    HiddenPairs /*....*/ = 04,

    [Display(Name = "n2")]
    NakedPairs /*.....*/ = 05,

    [Display(Name = "xwing")]
    XWing /*..........*/ = 06,

    [Display(Name = "h3")]
    HiddenTriples /*..*/ = 07,
    
    [Display(Name = "n3")]
    NakedTriples /*...*/ = 08,

    [Display(Name = "sky")]
    Skyscraper /*.....*/ = 09,

    [Display(Name = "kite")]
    TwoStringKite/*...*/ = 10,

    [Display(Name = "crane")]
    Crane/*...........*/ = 11,

    [Display(Name = "sfish")]
    Swordfish /*......*/ = 12,

    [Display(Name = "h4")]
    HiddenQuads /*....*/ = 13,

    [Display(Name = "n4")]
    NakedQuads /*.....*/ = 14,

    [Display(Name = "jfish")]
    Jellyfish /*......*/ = 15,
}
