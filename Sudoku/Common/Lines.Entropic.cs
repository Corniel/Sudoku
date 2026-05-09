namespace Sudoku.Common;

public static partial class Lines
{
    public static Rules Entropic(string grid)
        => Parse(grid).SelectMany(line => EntropicLine.New([.. line]));
}
