namespace Sudoku.Common;

public static class DoubleArrows
{
    public static IEnumerable<Restriction> Parse(string str)
        => Lines.Parse(str).SelectMany(DoubleArrow.New);
}
