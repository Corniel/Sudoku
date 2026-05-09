namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules DutchWhispers(string grid)
        => Parse(grid)
        .SelectMany(DutchWhisper.New);
}
