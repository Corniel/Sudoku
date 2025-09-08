namespace SudokuSolver.Constraints;

public sealed class Col(int col) : House(col, PosSet.New(range().Select(row => new Pos(row, col))))
{
    public static readonly ImmutableArray<Col> All = [.. range().Select(i => new Col(i))];
}
