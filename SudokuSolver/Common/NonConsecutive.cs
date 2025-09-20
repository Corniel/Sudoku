using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class NonConsecutive(Pos one, Pos two) : Rule
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reducer(one, two),
        new Reducer(two, one),
    ];

    public sealed class Reducer(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override Candidates Restrict(Cells cells) => Reduction[cells[Other]];

        private const int _ = 0;

        private static readonly ImmutableArray<Candidates> Reduction =
        [
            /* 0 */ Candidates._1_to_9,
            /* 1 */ Candidates.New(_, _, 3, 4, 5, 6, 7, 8, 9),
            /* 2 */ Candidates.New(_, _, _, 4, 5, 6, 7, 8, 9),
            /* 3 */ Candidates.New(1, _, _, _, 5, 6, 7, 8, 9),
            /* 4 */ Candidates.New(1, 2, _, _, _, 6, 7, 8, 9),
            /* 5 */ Candidates.New(1, 2, 3, _, _, _, 7, 8, 9),
            /* 6 */ Candidates.New(1, 2, 3, 4, _, _, _, 8, 9),
            /* 7 */ Candidates.New(1, 2, 3, 4, 5, _, _, _, 9),
            /* 8 */ Candidates.New(1, 2, 3, 4, 5, 6, _, _, _),
            /* 9 */ Candidates.New(1, 2, 3, 4, 5, 6, 7, _, _),
        ];
    }
}
