using SudokuSolver.Parsing;

namespace SudokuSolver.Common;

public static class EntropicLines
{
    public static IEnumerable<EntropicLine> Parse(string str)
        => Lines.Parse(str).Select(line => new EntropicLine(line));
}
