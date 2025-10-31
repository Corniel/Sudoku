namespace Sudoku.Restrictions;

public abstract partial class Cage(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
{
    protected Digits Restrict(SudokuCells graph, int sum)
    {
        var known = Digits.None;

        foreach (var cell in Others)
            known |= graph[cell].Digit;

        return Lookup[Others.Length + 1][sum][known];
    }
}
