namespace StrategyBased;

public sealed record ReduceOptions
{
    public bool AddCages { get; init; }

    public bool HiddenSingles { get; init; }

    public bool HiddenPairs { get; init; }

    public bool HiddenTriples { get; init; }

    public bool HiddenQuads { get; init; }

    public bool NakedPairs { get; init; }

    public bool NakedTriples { get; init; }

    public bool NakedQuads { get; init; }

    public bool Restrictions { get; init; }

    public bool PointingDigits { get; init; }

    public bool XWing { get; init; }

    public bool Swordfish { get; init; }

    public bool Jellyfish { get; init; }

    public bool DynamicSolver { get; init; }

    public bool Log { get; init; }

    public static readonly ReduceOptions All = new()
    {
        AddCages = true,
        NakedPairs = true,
        NakedTriples = true,
        NakedQuads = true,
        HiddenSingles = true,
        HiddenPairs = true,
        HiddenTriples = true,
        HiddenQuads = true,
        PointingDigits = true,
        Restrictions = true,
        XWing = true,
        Swordfish = true,
        Jellyfish = false,
    };
}
