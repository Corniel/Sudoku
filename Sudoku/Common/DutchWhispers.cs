using Sudoku.Parsing;

namespace Sudoku.Common;

public static class DutchWhispers
{
    public static IEnumerable<DutchWhisper> Parse(string str)
        => Lines.Parse(str).Select(line => new DutchWhisper(line));
}
