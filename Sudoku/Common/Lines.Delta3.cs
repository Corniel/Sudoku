namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Along Delta-3 lines, digits must differ from their neighbors by
    /// at least 3.
    /// </summary>
    /// <example>
    /// ...│.7.│...
    /// ...│.4.│...
    /// ...│.1.│...
    /// ───┼───┼───
    /// ...│.58│2..
    /// ...│...│9..
    /// ...│...│...
    /// ───┼───┼───
    /// ...│...│...
    /// ...│...│...
    /// ...│...│...
    /// </example>
    [Pure]
    public static Rules Delta3(string grid) => Parse(grid).SelectMany(Delta3);

    private static Rules Delta3(Line line)
       => range(line.Length - 1)
       .SelectMany(f => DeltaMin.New(line[f + 0], line[f + 1], 3));
}
