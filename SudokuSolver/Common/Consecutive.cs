using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class Consecutive(Pos one, Pos two) : Rule
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [one, two];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Reducer(one, two),
        new Reducer(two, one),
    ];

    public override string ToString() => $"{Cells.First()} = {Cells.Last()} ± 1";

    public sealed class Reducer(Pos appliesTo, Pos other) : Pair(appliesTo, other)
    {
        public override Candidates Restrict(Cells cells) => Reduction[cells[Other]];

        private const int _ = 0;

        private static readonly ImmutableArray<Candidates> Reduction =
        [
            /* 0 */ Candidates._1_to_9,
            /* 1 */ Candidates.New(_, 2, _, _, _, _, _, _, _),
            /* 2 */ Candidates.New(1, _, 3, _, _, _, _, _, _),
            /* 3 */ Candidates.New(_, 2, _, 4, _, _, _, _, _),
            /* 4 */ Candidates.New(_, _, 3, _, 5, _, _, _, _),
            /* 5 */ Candidates.New(_, _, _, 4, _, 6, _, _, _),
            /* 6 */ Candidates.New(_, _, _, _, 5, _, 7, _, _),
            /* 7 */ Candidates.New(_, _, _, _, _, 6, _, 8, _),
            /* 8 */ Candidates.New(_, _, _, _, _, _, 7, _, 9),
            /* 9 */ Candidates.New(_, _, _, _, _, _, _, 8, _),
        ];
    }
}
