using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Ratio1_2(Pos a, Pos b) : Set(a, b)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. new LookupPair(a, b, Lookup).Couple()
    ];

    public override string ToString() => $"{Cells.First()} : {Cells.Last()} = 1 : 2 or 2 : 1";

    private static readonly LookupDigits Lookup = LookupPair.Init(
    [
        /* 0 */ Digits._1_to_9,
        /* 1 */ [2],
        /* 2 */ [1, 4],
        /* 3 */ [6],
        /* 4 */ [2, 8],
        /* 5 */ default,
        /* 6 */ [3],
        /* 7 */ default,
        /* 8 */ [4],
        /* 9 */ default,
    ]);
}
