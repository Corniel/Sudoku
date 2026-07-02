using Sudoku.Restrictions;

namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>Amongst parity lines, cells alter between being even or odd.</summary>
    /// <example>
    /// ...│.6.│...
    /// ...│.1.│...
    /// ...│.7.│...
    /// ───┼───┼───
    /// ...│.25│4..
    /// ...│...│9..
    /// ...│...│...
    /// ───┼───┼───
    /// ...│...│...
    /// ...│...│...
    /// ...│...│...
    /// </example>
    [Pure]
    public static Rules Parity(string grid)
        => Parse(grid).SelectMany(ParityLine.New);
}
