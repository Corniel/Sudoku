namespace SudokuSolver.Houses;

public sealed class Box(int index) : House(index, box(index))
{
    public static readonly ImmutableArray<Box> All = [.. range().Select(i => new Box(i))];

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
