namespace Sudoku.Common;

public static partial class Groups
{
    /// <summary>Handles even/odd constraints.</summary>
    /// <remarks>
    /// [1-9]: regular clues
    /// E:     cell is even
    /// O:     cell is odd
    /// line:  all cells are either odd or even.
    /// .:     No restriction.
    /// </remarks>
    [Pure]
    public static Rules EvenOdd(string grid)
        => Grid.NamedGroups(grid).SelectMany(EvenOdd);

    private static Rules EvenOdd(NamedGroup group) => group.Name switch
    {
        var n when char.IsAsciiDigit(n) => group.Select(c => new Mask(c, [n - '0'])),
        'E' => group.Select(Mask.Even),
        'O' => group.Select(Mask.Odd),
        _ => Dominos.RoundRobin([.. group]).SelectMany(domino => new LookupPair(domino, Parity).Couple()),
    };

    private static readonly LookupDigits Parity = LookupPair.Init(d => d.IsEven() ? Digits.Even : Digits.Odd);
}
