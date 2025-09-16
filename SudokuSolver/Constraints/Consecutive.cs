using SudokuSolver.Parsing;

namespace SudokuSolver.Constraints;

[DebuggerDisplay("{Cells.First()} = {Cells.Last()} ± 1")]
public sealed class Consecutive(Pos one, Pos two) : Constraint
{
    public static ImmutableArray<Consecutive> Parse(string str) =>
    [
        .. NamedCage.Parse(str)
        .Where(c => c.Cells.Length is 2)
        .Select(c => new Consecutive(c.Cells[0], c.Cells[1]))
    ];

    public override bool IsSet => true;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reducer(one, two),
        new Reducer(two, one),
    ];

    public sealed class Reducer(Pos applies, Pos other) : Restriction
    {
        public Pos AppliesTo { get; } = applies;

        public Pos Other { get; } = other;

        public Candidates Restrict(Cells cells) => Reduction[cells[Other]];

        private const int _ = 0;

        private static readonly ImmutableArray<Candidates> Reduction =
        [
            /* 0 */ Candidates._1_to_9,
            /* 1 */ Candidates.New(1, 2, _, _, _, _, _, _, _),
            /* 2 */ Candidates.New(1, 2, 3, _, _, _, _, _, _),
            /* 3 */ Candidates.New(_, 2, 3, 4, _, _, _, _, _),
            /* 4 */ Candidates.New(_, _, 3, 4, 5, _, _, _, _),
            /* 5 */ Candidates.New(_, _, _, 4, 5, 6, _, _, _),
            /* 6 */ Candidates.New(_, _, _, _, 5, 6, 7, _, _),
            /* 7 */ Candidates.New(_, _, _, _, _, 6, 7, 8, _),
            /* 8 */ Candidates.New(_, _, _, _, _, _, 7, 8, 9),
            /* 9 */ Candidates.New(_, _, _, _, _, _, _, 8, 9),
        ];
    }
}
