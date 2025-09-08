namespace SudokuSolver.Constraints;

public sealed record NonConsecutive : Restriction
{
    public NonConsecutive(Pos appliesTo, params ImmutableArray<Pos> involved)
    {
        AppliesTo = appliesTo;
        Involved = involved;
    }

    public override Candidates Restrict(Cells cells)
    {
        Candidates values = Candidates.All;
        foreach (var other in Involved)
        {
            values &= Reduction[cells[other]];
        }

        return values;
    }

    private const int _ = 0;

    private static readonly ImmutableArray<Candidates> Reduction =
    [
        /* 0 */ Candidates.All,
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
