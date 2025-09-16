namespace SudokuSolver.Constraints;

public sealed class GermanWhisper(Pos one, Pos two) : Constraint
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Neighbors(one, two),
        new Neighbors(two, one),
    ];

    public override string ToString() => $"German whispers = {string.Join(", ", Cells)}";

    public sealed class Neighbors(Pos appliesTo, Pos neighbor) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public Pos Neigbor { get; } = neighbor;

        public override string ToString() => $"{AppliesTo} = {Neigbor} ± 5";

        public Candidates Restrict(Cells cells) => Allowed[cells[Neigbor]];

        private static readonly ImmutableArray<Candidates> Allowed =
        [
            /* ? */ [1, 2, 3, 4, 6, 7, 8, 9],
            /* 1 */ [6, 7, 8, 9],
            /* 2 */ [7, 8, 9],
            /* 3 */ [8, 9],
            /* 4 */ [9],
            /* 5 */ [],
            /* 6 */ [1],
            /* 7 */ [1, 2],
            /* 8 */ [1, 2, 3],
            /* 9 */ [1, 2, 3, 4],
        ];
    }
}
