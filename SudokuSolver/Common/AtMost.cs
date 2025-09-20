using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class AtMost(Pos one, Pos two, int sum) : Rule
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reducer(one, two, sum),
        new Reducer(two, one, sum),
    ];

    [DebuggerDisplay("{AppliesTo} + {Other} <= {Sum}")]
    public sealed class Reducer(Pos appliesTo, Pos other, int sum) : Pair(appliesTo, other)
    {
        public int Sum { get; } = sum;

        public override Candidates Restrict(Cells cells) => Candidates.AtMost(Sum - cells[Other]);
    }
}
