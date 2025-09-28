using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class Consecutive(Pos one, Pos two) : Rule
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new DeltaMax(one, two, 1),
        new DeltaMax(two, one, 1),
    ];

    public override string ToString() => $"{Cells.First()} = {Cells.Last()} ± 1";
}
