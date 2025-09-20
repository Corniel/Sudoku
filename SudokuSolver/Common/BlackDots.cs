using SudokuSolver.Parsing;

namespace SudokuSolver.Common;

public static class BlackDots
{
    public static ImmutableArray<Ratio1_2> Parse(string str) =>
    [
        .. NamedCage.Parse(str)
        .Where(c => c.Cells.Length is 2)
        .Select(c => new Ratio1_2(c.Cells[0], c.Cells[1]))
    ];
}
