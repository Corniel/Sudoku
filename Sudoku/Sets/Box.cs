namespace Sudoku.Sets;

public sealed class Box(int index, PosSet set) : House(index, set)
{
    public static int IndexOf(Pos pos)
    {
        var (r, c) = pos;
        return ((r / 3) * 3) + (c / 3);
    }

    internal static IEnumerable<Box> All() => range().Select(i => new Box(i, New(i)));

    private static PosSet New(int index)
    {
        var box = PosSet.Empty;

        var (row, col) = Math.DivRem(index, 3);
        row *= 3;
        col *= 3;

        for (var r = row; r < row + 3; r++)
            for (var c = col; c < col + 3; c++)
                box |= (r, c);

        return box;
    }
}
