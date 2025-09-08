namespace SudokuSolver.Constraints;

public sealed class Row(int row) : House(row, PosSet.New(range().Select(col => new Pos(row, col))))
{
    public static readonly ImmutableArray<Row> All = [.. range().Select(i => new Row(i))];
}
