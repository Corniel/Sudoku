namespace Sudoku.Common;

public static class GermanWhispers
{
    public static IEnumerable<GermanWhisper> Parse(string str)
        => Lines.Parse(str).Select(line => new GermanWhisper(line));
}
