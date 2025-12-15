using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class SumsOfTens
{
    [Pure]
    public static IEnumerable<SumsOfTen> Parse(string str)
        => Lines.Parse(str)
        .SelectMany(line => Group.Select(line, (a, _) => new SumsOfTen(a, line)));

}
