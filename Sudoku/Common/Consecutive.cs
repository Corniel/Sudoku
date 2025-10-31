using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Consecutive(Pos one, Pos two) : Set(one, two)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new DeltaMax(one, two, 1),
        new DeltaMax(two, one, 1),
    ];

    public override string ToString() => $"{Cells.First()} = {Cells.Last()} ± 1";
}
