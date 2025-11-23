using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class EntropicLine
{
    public static IEnumerable<Restriction> New(ImmutableArray<Pos> line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            for (var s = f + 1; s < line.Length; s++)
            {
                var lookup = (s - f) % 3 == 0 ? Same : Diff;
                var pairs = new LookupPair(line[f], line[s], lookup).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([[1, 2, 3], [4, 5, 6], [7, 8, 9,]]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([[1, 2, 3], [4, 5, 6], [7, 8, 9,]]);
}
