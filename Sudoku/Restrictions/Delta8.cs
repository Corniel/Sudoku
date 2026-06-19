namespace Sudoku.Restrictions;

public static class Delta8
{
    public static Rules New(Line line)
    {
        for (var f = 0; f < line.Length - 1; f++)
        {
            for (var s = 2; s < line.Length; s++)
            {
                var lookup = (s - f).IsEven() ? Same : Diff;
                var pairs = new LookupPair(line[f], line[s], lookup).Couple();
                yield return pairs.One;
                yield return pairs.Two;
            }
        }
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([[1], [9]]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([[1], [9]]);
}
