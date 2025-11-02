namespace Sudoku.Common;

public static class Arrows
{
    public static IEnumerable<Arrow> ParseSets(string str)
        => Lines.Parse(str)
        .Select(line => new Arrow(line, true));
}
