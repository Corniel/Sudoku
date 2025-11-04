namespace Sudoku.Common;

public static class Arrows
{
    public static IEnumerable<Arrow> Parse(string str)
        => Lines.Parse(str)
        .Select(line => new Arrow(line));
}
