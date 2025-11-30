using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class Arrows
{
    public static RulesExtender Parse(string str) => rules =>
    {
        var lines = Lines.Parse(str).ToList();
        return rules
            + lines.Select(line => new Arrow(line))
            + lines.SelectMany(l => AsSet(l, rules.Sets));
    };

    private static IEnumerable<Mask> AsSet(ImmutableArray<Pos> line, IEnumerable<PosSet> sets)
    {
        var cells = PosSet.New(line[1..]);
        if (sets.Any(cells.IsSubsetOf))
        {
            yield return new Mask(line[0], Digits.AtLeast(triangle(line.Length - 1)));
        }
    }
}
