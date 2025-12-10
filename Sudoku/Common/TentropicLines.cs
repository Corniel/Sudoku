namespace Sudoku.Common;

public static class TentropicLines
{
    public static IEnumerable<Restriction> Parse(string str)
        => Lines.Parse(str).SelectMany(TentropicLine.New);
}
