using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class NonConsecutive
{
    public static ImmutableArray<Restriction> New(Domino d) => New(d.A, d.B);

    public static ImmutableArray<Restriction> New(Pos one, Pos two) =>
    [
        .. new LookupPair(one, two, Lookup).Couple(),
        new Unique(one, [two]),
        new Unique(two, [one]),
    ];

    private static readonly LookupDigits Lookup = LookupPair.Init(d => ~Digits.Between(d - 1, d + 1));
}
