namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>The digits on a between line should all be within the range of the two end digits.</summary>
    public static Rules Between(string grid)
        => Parse(grid).SelectMany(BetweenLine.New);
}
