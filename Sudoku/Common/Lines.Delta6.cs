namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Along Delta-6 lines, digits must differ from their neighbors by
    /// at least 6. As result, they can not contain the digits 4, 5 and, 6, and neighbors
    /// toggle from high to low.
    /// </summary>
    /// <example>
    /// ...│.7.│...
    /// ...│.1.│...
    /// ...│.8.│...
    /// ───┼───┼───
    /// ...│.29│3..
    /// ...│...│9..
    /// ...│...│...
    /// ───┼───┼───
    /// ...│...│...
    /// ...│...│...
    /// ...│...│...
    /// </example>
    [Pure]
    public static Rules Delta6(string grid) => Parse(grid).SelectMany(Delta6);

    private static Rules Delta6(Line line) =>
    [
        .. Restrictions.Delta6.New(line),
        .. line.Select(c => new Mask(c, _123__789)),
    ];

    private static readonly Digits _123__789 = [1, 2, 3, 7, 8, 9];
}
