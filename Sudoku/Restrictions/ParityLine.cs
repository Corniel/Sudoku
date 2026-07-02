namespace Sudoku.Restrictions;

public static class ParityLine
{
    public static Rules New(Line line)
    {
        for (var f = 0; f < line.Length; f++)
            for (var s = 0; s < line.Length; s++)
                if (f != s)
                    yield return new LookupPair(line[f], line[s], (f - s).IsEven() ? Same : Diff);
    }

    private static readonly LookupDigits Same = LookupPair.SameClass([Digits.Even, Digits.Odd]);
    private static readonly LookupDigits Diff = LookupPair.DiffClass([Digits.Even, Digits.Odd]);
}
