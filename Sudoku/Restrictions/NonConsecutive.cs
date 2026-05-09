namespace Sudoku.Restrictions;

public static class NonConsecutive
{
    public static Rules New(Domino d) => New(d.A, d.B);

    public static Rules New(Pos one, Pos two) =>
    [
        .. new LookupPair(one, two, Lookup).Couple(),
        new CellSet([one, two], "Non-consecutive"),
    ];

    private static readonly LookupDigits Lookup = LookupPair.Init(d => ~Digits.Between(d - 1, d + 1));
}
