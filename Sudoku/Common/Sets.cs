namespace Sudoku.Common;

public static class Sets
{
    public static IEnumerable<Set> Parse(string str)
        => NamedCage.Parse(str).Select(line => new Set(line.Cells));
}
