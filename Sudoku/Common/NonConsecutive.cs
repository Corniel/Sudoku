using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class NonConsecutive(Pos one, Pos two) : Set(one, two)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. new LookupPair(one, two, Lookup).Couple()
    ];

    public override string ToString() => $"{Cells.First()} != {Cells.Last()} ± 1";

    private static readonly LookupDigits Lookup = LookupPair.Init(d => ~Digits.Between(d - 1, d + 1));
}
