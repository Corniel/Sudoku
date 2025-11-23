namespace Sudoku.Common;

public static class GermanWhispers
{
    public static IEnumerable<Restriction> Parse(string str)
        => Lines.Parse(str).SelectMany(GermanWhisper.New);
}
