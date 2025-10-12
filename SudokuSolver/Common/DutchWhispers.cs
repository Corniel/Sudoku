using SudokuSolver.Parsing;

namespace SudokuSolver.Common;

public static class DutchWhispers
{
    public static IEnumerable<DutchWhisper> Parse(string str)
        => Lines.Parse(str).Select(line => new DutchWhisper(line));
}
