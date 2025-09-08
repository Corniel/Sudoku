namespace SudokuSolver;

/// <summary>A house is a group of 9 cells which must contain digits 1 through 9.</summary>
public static class Houses
{
    public static class Diagonal
    {
        public static readonly PosSet NW_SE = PosSet.New(range().Select(i => new Pos(i, i)));
        public static readonly PosSet NE_SW = PosSet.New(range().Select(i => new Pos(i, _9 - i - 1)));
    }

    public static readonly ImmutableArray<PosSet> Row = [.. range().Select(row => PosSet.New(range().Select(col => new Pos(row, col))))];

    public static readonly ImmutableArray<PosSet> Col = [.. range().Select(col => PosSet.New(range().Select(row => new Pos(row, col))))];

    public static readonly ImmutableArray<PosSet> Box = [.. range().Select(box)];

    public static readonly ImmutableArray<PosSet> Standard = [.. Row, .. Col, .. Box];

    private static IEnumerable<int> range() => Enumerable.Range(0, 9);

    private static PosSet box(int i)
    {
        var box = PosSet.Empty;

        var (row, col) = Math.DivRem(i, 3);
        row *= 3;
        col *= 3;

        for (var r = row; r < row + 3; r++)
            for (var c = col; c < col + 3; c++)
                box |= (r, c);

        return box;
    }
}
