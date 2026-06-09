namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>All digits on a line are eighter even, or odd.</summary>
    public static Rules SameParity(string grid) => Grid
        .NamedGroups(grid)
        .SelectMany(line => Dominos.RoundRobin([.. line]))
        .SelectMany(domino => new LookupPair(domino, Parity).Couple());

    private static readonly LookupDigits Parity = LookupPair.Init(d => d.IsEven() ? Digits.Even : Digits.Odd);
}
