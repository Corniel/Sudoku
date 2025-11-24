namespace Sudoku.Common;

public static class ZipLine
{
    public static IEnumerable<Arrow> New(ImmutableArray<Pos> line)
    {
        var mid = line.Length / 2;
        var circle = line[mid];

        var i = 1;
        while (i + mid < line.Length)
        {
            yield return new Arrow([circle, line[mid - i], line[mid + i]]);
            i++;
        }
    }
}
