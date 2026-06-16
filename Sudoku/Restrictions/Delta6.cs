namespace Sudoku.Restrictions;

public static class Delta6
{
    public static Rules New(Line line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            var min6 = DeltaMin.New(line[f + 0], line[f + 1], 6);
            yield return min6.One;
            yield return min6.Two;

            for (var s = 2; s < line.Length; s++)
            {
                var lookup = (s - f).IsEven() ? Same : Diff;
                var pairs = new LookupPair(line[f], line[s], lookup).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([1..3, 7..9]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([1..3, 7..9]);
}
