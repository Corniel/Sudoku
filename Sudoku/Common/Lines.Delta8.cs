namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Along Delta-8 lines, digits toggle between 1 and 9.
    /// </summary>
    /// <example>
    /// ...│...│...
    /// ...│...│..1
    /// ...│...│.9.
    /// ───┼───┼───
    /// ...│...│1..
    /// ...│..9│...
    /// ...│.1.│...
    /// ───┼───┼───
    /// ...│9..│...
    /// ..1│...│...
    /// .9.│...│...
    /// </example>
    [Pure]
    public static Rules Delta8(string grid) => Parse(grid).SelectMany(Delta8);

    private static Rules Delta8(Line line) =>
    [
        .. Restrictions.Delta8.New(line),
        .. line.Select(c => new Mask(c, _1_or_9)),
    ];

    private static readonly Digits _1_or_9 = [1, 9];
}
