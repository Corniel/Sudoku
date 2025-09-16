using SudokuSolver.Parsing;

namespace SudokuSolver.Constraints;

public static class WhiteDots
{
    public static ImmutableArray<Consecutive> Parse(string str) =>
    [
        .. NamedCage.Parse(str)
        .Where(c => c.Cells.Length is 2)
        .Select(c => new Consecutive(c.Cells[0], c.Cells[1]))
    ];
}
