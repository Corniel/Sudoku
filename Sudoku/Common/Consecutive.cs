using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Consecutive(Pos one, Pos two) : Set(one, two)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. new LookupPair(one, two, Lookup).Couple()
    ];

    public override string ToString() => $"{Cells.First()} = {Cells.Last()} ± 1";

    private static readonly LookupDigits Lookup = LookupPair.Init(d => [d - 1, d + 1]);
}
