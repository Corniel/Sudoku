using SudokuSolver.Parsing;
using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public static class Parity
{
    public static IEnumerable<Mask> Parse(string str)
    {
        var cages = NamedCage.Parse(str);
        var even = cages.FirstOrDefault(c => c.Name is 'E')?.Cells ?? [];
        var odd = cages.FirstOrDefault(c => c.Name is 'O')?.Cells ?? [];

        return
        [
            .. even.Select(cell => new Mask(cell, Candidates.Even)),
            .. odd.Select(cell => new Mask(cell, Candidates.Odd)),
        ];
    }
}
