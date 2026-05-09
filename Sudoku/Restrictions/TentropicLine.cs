namespace Sudoku.Restrictions;

public static class TentropicLine
{
    public static Rules New(Line line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            for (var s = f + 1; s < line.Length; s++)
            {
                var lookup = (s - f) % 4 == 0 ? Same : Diff;
                var pairs = new LookupPair(line[f], line[s], lookup).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([[1, 9], [2, 8], [3, 7], [4, 6]]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([[1, 9], [2, 8], [3, 7], [4, 6]]);
}
