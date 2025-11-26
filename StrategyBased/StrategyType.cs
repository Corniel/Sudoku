namespace StrategyBased;

public enum StrategyType
{
    None = 0,

    NakedSingles /*...*/ = 01,
    HiddenSingles /*..*/ = 02,

    PointingDigits /*.*/ = 03,
    
    HiddenPairs /*....*/ = 04,
    NakedPairs /*.....*/ = 05,
    
    XWing /*..........*/ = 06,
    
    HiddenTriples /*..*/ = 07,
    NakedTriples /*...*/ = 08,
    
    Swordfish /*......*/ = 09,
    
    HiddenQuads /*....*/ = 10,
    NakedQuads /*.....*/ = 11,
}
