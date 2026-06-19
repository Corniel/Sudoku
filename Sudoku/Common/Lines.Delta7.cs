namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Along Delta-7 lines, digits must differ from their neighbors by
    /// at least 7. As result, they can only contain the digits 1, 2, 8, and 9.
    /// </summary>
    /// <example>
    /// ...│.8.│...
    /// ...│.1.│...
    /// ...│.9.│...
    /// ───┼───┼───
    /// ...│..2│...
    /// ...│...│9..
    /// ...│...│...
    /// ───┼───┼───
    /// ...│...│...
    /// ...│...│...
    /// ...│...│...
    /// </example>
    [Pure]
    public static Rules Delta7(string grid) => Parse(grid).SelectMany(Delta7);

    private static Rules Delta7(Line line) =>
    [
        .. Restrictions.Delta7.New(line),
        .. line.Select(c => new Mask(c, _12_89)),
    ];

    private static readonly Digits _12_89 = [1, 2, 8, 9];
}
