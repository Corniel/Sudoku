using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class GermanWhispers
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
    public static RulesExtender Parse(string str) => rules =>
    {
        var lines = Lines.Parse(str);

        return rules
            + lines.SelectMany(GermanWhisper.New)
            + lines.SelectMany(line => Masks(line, rules));
    };

    private static IEnumerable<Mask> Masks(ImmutableArray<Pos> line, Rules rules)
    {
        for (var i = 1; i < line.Length - 1; i++)
        {
            var pos = line[i];
            PosSet group = [line[i - 1], pos, line[i + 1]];
            if (rules.Sets.Any(group.IsSubsetOf))
                yield return new Mask(pos, _123789);
        }
    }

    private static readonly Digits _123789 = [1, 2, 3, 7, 8, 9];
}
