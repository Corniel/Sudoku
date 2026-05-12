using Sudoku.Restrictions;

namespace Sudoku.Common;

public static partial class Lines
{
    public static IEnumerable<Restriction> Palindrome(string grid)
    {
        var lines = Parse(grid);

        foreach (var line in lines)
        {
            for (var i = 0; i < line.Length / 2; i++)
            {
                var (f, s) = (line[i], line[^(i + 1)]);
                yield return new Twin(f, s);
                yield return new Twin(s, f);
            }
        }
    }
}
