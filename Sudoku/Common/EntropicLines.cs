namespace Sudoku.Common;

public static class EntropicLines
{
    public static IEnumerable<Restriction> Parse(string str)
        => Lines.Parse(str).SelectMany(EntropicLine.New);
}
