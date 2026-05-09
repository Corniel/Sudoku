namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// A long German whisper lines, digits must differ from their neighbors by
    /// at least 5. As result, they can not contain the digit 5, and neighbors
    /// toggle from high to low.
    /// </summary>
    /// <example>
    /// ...│.6.│...
    /// ...│.1.│...
    /// ...│.7.│...
    /// ───┼───┼───
    /// ...│.28│1..
    /// ...│...│6..
    /// ...│...│...
    /// ───┼───┼───
    /// ...│...│...
    /// ...│...│...
    /// ...│...│...
    /// </example>
    [Pure]
    public static RulesExtender GermanWhisper(string grid) => rules =>
    {
        var lines = Parse(grid).ToList();
        return rules
            + lines.SelectMany(Restrictions.GermanWhisper.New)
            + lines.SelectMany(line => Masks(line, rules));
    };

    /// <summary>
    /// Gets the masks for indvidual cells.
    /// </summary>
    /// <remarks>
    /// Not allowed
    /// * 5
    /// * 4 or 6
    ///   when both neighbors are in the same set:
    ///   94? or 14?
    /// * 1 or 9
    ///   when all 3 previous and next 3 are in the same set.
    /// </remarks>
    private static IEnumerable<Mask> Masks(Line line, RuleSet rules)
    {
        return range(line.Length)
            .Select(i => new Mask(line[i], digits(i)));

        Digits digits(int i)
        {
            var mask = _12346789;
            if (i >= 1 && i < line.Length - 1)
            {
                var pos = line[i];
                PosSet group = [line[i - 1], pos, line[i + 1]];
                if (rules.Sets.Any(group.IsSubsetOf))
                    mask ^= _46;
            }
            if (i >= 3 && i < line.Length - 3)
            {
                var pos = line[i];
                PosSet group = [line[i - 3], line[i - 2], line[i - 1], pos, line[i + 1], line[i + 2], line[i + 3]];

                if (rules.Sets.Any(group.IsSubsetOf))
                    mask ^= _19;
            }
            return mask;
        }
    }

    private static readonly Digits _19 = [1, 9];
    private static readonly Digits _46 = [4, 6];
    private static readonly Digits _12346789 = [1, 2, 3, 4, 6, 7, 8, 9];
}
