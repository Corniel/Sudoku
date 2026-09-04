namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Along Non-consecutive ( Delta-2) lines, digits must differ from their
    /// neighbors by at least 2.
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
    public static Rules NonConsecutive(string grid) => Parse(grid).SelectMany(Delta2);

    private static Rules Delta2(Line line)
       => range(line.Length - 1)
       .SelectMany(f => DeltaMin.New(line[f + 0], line[f + 1], 2));
}
