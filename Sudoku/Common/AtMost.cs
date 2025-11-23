using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class AtMost
{
    public static Couple<Pair> New(Pos one, Pos two, int sum)
        => new LookupPair(one, two, Lookups[sum]).Couple();

    public static readonly ImmutableArray<LookupDigits> Lookups =
    [
        .. range(_9 + _9).Select(sum => LookupPair.Init(d => Digits.AtMost(sum - d)))
    ];
}
