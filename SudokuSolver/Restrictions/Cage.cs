namespace SudokuSolver.Restrictions;

public abstract partial class Cage(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
{
    protected Candidates Restrict(Cells cells, int sum)
    {
        var known = Candidates.None;

        foreach (var cell in Others)
            known |= cells[cell];

        return Lookup[Others.Length + 1][sum][known];
    }
}
