using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class GermanWhisper
{
    public static IEnumerable<Restriction> New(ImmutableArray<Pos> line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            var min5 = DeltaMin.New(line[f + 0], line[f + 1], 5);
            yield return min5.One;
            yield return min5.Two;

            for (var s = 2; s < line.Length; s++)
            {
                var lookup = (s - f).IsEven() ? Same : Diff;
                var pairs = new LookupPair(line[f], line[s], lookup).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([[1, 2, 3, 4], [6, 7, 8, 9,]]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([[1, 2, 3, 4], [6, 7, 8, 9,]]);
}
