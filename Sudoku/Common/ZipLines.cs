namespace Sudoku.Common;

public static class ZipLines
{
    public static IEnumerable<Arrow> Parse(string str)
        => Lines.Parse(str).SelectMany(ZipLine.New);
}
