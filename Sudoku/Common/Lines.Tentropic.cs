namespace Sudoku.Common;

public static partial class Lines
{
    [Pure]
    public static Rules Tentropic(string grid)
        => Parse(grid).SelectMany(TentropicLine.New);
}
