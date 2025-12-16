using Sudoku.Restrictions;

namespace Sudoku.Common;

/// <summary>
/// Quadruple define for the 4 cells they overlap which digit(s) should at least occur.
/// </summary>
/// Where the quadruple requires 148:
///
/// ...│...│...
/// ...│...│...
/// ...│12.│...
/// ───┼───┼───
/// ...│48.│...
/// ...│...│...
/// ...│...│...
/// ───┼───┼───
/// ...│...│...
/// ...│...│...
/// ...│...│...
/// </example>
public static class Quadruples
{
    public static IEnumerable<Restriction> Parse(string str)
        => NamedCage.Parse(str)
        .SelectMany(c => Group.Select(c.Cells, (a, o) => new Quadruple(a, o, AsDigits(c.Sum))));

    private static Digits AsDigits(int num)
    {
        var digits = Digits.None;
        while (num > 0)
        {
            digits |= num % 10;
            num /= 10;
        }
        return digits;
    }
}
