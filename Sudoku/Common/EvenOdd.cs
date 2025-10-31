using Sudoku.Parsing;
using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class EvenOdd
{
    public static IEnumerable<Mask> Parse(string str)
    {
        var cages = NamedCage.Parse(str);
        var even = cages.FirstOrDefault(c => c.Name is 'E')?.Cells ?? [];
        var odd = cages.FirstOrDefault(c => c.Name is 'O')?.Cells ?? [];

        return
        [
            .. even.Select(cell => new Mask(cell, Digits.Even)),
            .. odd.Select(cell => new Mask(cell, Digits.Odd)),
        ];
    }
}
