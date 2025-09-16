namespace SudokuSolver.Constraints;

public sealed class Ratio1_2(Pos a, Pos b) : Constraint
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [a, b];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reduce(a, b),
        new Reduce(b, a),
    ];

    public override string ToString() => $"{Cells.First()} : {Cells.Last()} = 1 : 2 or 2 : 1";

    public sealed class Reduce(Pos appliesTo, Pos other) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public Pos Other { get; } = other;

        public Candidates Restrict(Cells cells) => Lookup[cells[Other]];

        private static readonly ImmutableArray<Candidates> Lookup =
        [
            /* 0 */ Candidates._1_to_9,
            /* 1 */ [2],
            /* 2 */ [1, 4],
            /* 3 */ [6],
            /* 4 */ [2, 8],
            /* 5 */ default,
            /* 6 */ [3],
            /* 7 */ default,
            /* 8 */ [4],
            /* 9 */ default,
        ];
    }
}
