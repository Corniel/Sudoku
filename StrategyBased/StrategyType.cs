using System.ComponentModel.DataAnnotations;

namespace StrategyBased;

public enum StrategyType
{
    None = 0,

    NakedSingles /*...*/ = 01,
    HiddenSingles /*..*/ = 02,

    PointingDigits /*.*/ = 03,

    [Display(Name = "h2")]
    HiddenPairs /*....*/,

    [Display(Name = "n2")]
    NakedPairs /*.....*/,

    [Display(Name = "xwing")]
    XWing /*..........*/,

    [Display(Name = "xywing")]
    XYWing,

    [Display(Name = "h3")]
    HiddenTriples /*..*/,
    
    [Display(Name = "n3")]
    NakedTriples /*...*/,

    [Display(Name = "sky")]
    Skyscraper /*.....*/,

    [Display(Name = "kite")]
    TwoStringKite/*...*/,

    [Display(Name = "crane")]
    Crane/*...........*/,

    [Display(Name = "sfish")]
    Swordfish /*......*/,

    [Display(Name = "h4")]
    HiddenQuads /*....*/,

    [Display(Name = "n4")]
    NakedQuads /*.....*/,

    [Display(Name = "jfish")]
    Jellyfish /*......*/,
}
