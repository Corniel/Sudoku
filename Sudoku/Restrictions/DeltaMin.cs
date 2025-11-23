namespace Sudoku.Restrictions;

public static class DeltaMin
{
    public static Couple<Pair> New(Pos one, Pos two, int delta)
        => new LookupPair(one, two, Lookups[delta]).Couple();

    public static readonly ImmutableArray<LookupDigits> Lookups =
    [
        .. range(_9).Select(delta => LookupPair.Init(d => ~Digits.Between(d - delta + 1, d + delta - 1)))
    ];
}
