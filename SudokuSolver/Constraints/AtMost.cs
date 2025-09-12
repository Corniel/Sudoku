namespace SudokuSolver.Constraints;

public sealed class AtMost(Pos one, Pos two, int sum) : Constraint
{
    public override bool IsSet => false;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reducer(one, two, sum),
        new Reducer(two, one, sum),
    ];

    [DebuggerDisplay("{AppliesTo} + {Other} <= {Sum}")]
    public sealed class Reducer(Pos applies, Pos other, int sum) : Restriction
    {
        public Pos AppliesTo { get; } = applies;

        public Pos Other { get; } = other;

        public int Sum { get; } = sum;

        public Candidates Restrict(Cells cells) => Candidates.AtMost(Sum - cells[Other]);
    }
}
