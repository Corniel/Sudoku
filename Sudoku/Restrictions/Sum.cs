namespace Sudoku.Restrictions;

public static class Sum
{
    public static Couple<Pair> New(Pos one, Pos two, int sum)
        => new LookupPair(one, two, Lookups[sum]).Couple();

    private static readonly ImmutableArray<LookupDigits> Lookups =
    [
        .. range(_9 * 2).Select(sum => LookupPair.Init(d => [sum - d]))
    ];
}
