using SudokuSolver.Parsing;

namespace SudokuSolver.Constraints;

public static class RenbanLines
{
    public static IEnumerable<RenbanLine> Parse(string str)
        => NamedCage.Parse(str)
        .Select(cage => new RenbanLine(cage.Cells));
}
