namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules DutchWhisper(string grid)
        => Parse(grid)
        .SelectMany(Restrictions.DutchWhisper.New);
}
