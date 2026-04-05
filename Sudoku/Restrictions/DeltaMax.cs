namespace Sudoku.Restrictions;

public static class DeltaMax
{
    public static Couple<Pair> New(Domino domino, int delta)
        => New(domino.A, domino.B, delta);

    public static Couple<Pair> New(Pos one, Pos two, int delta)
        => new LookupPair(one, two, Lookups[delta]).Couple();

    private static readonly ImmutableArray<LookupDigits> Lookups =
    [
        .. range(_9).Select(delta => LookupPair.Init(d => Digits.Between(d - delta, d + delta)))
    ];
}
