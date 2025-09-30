using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class NonConsecutive(Pos one, Pos two) : Set(one, two)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new DeltaMin(one, two, 2),
        new DeltaMin(two, one, 2),
    ];
}
