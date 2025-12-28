using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class Twins
{
    public static IEnumerable<Twin> Parse(string str)
    {
        foreach (var cage in NamedCage.Parse(str).Select(c => c.Cells))
        {
            for (var i = 0; i < cage.Length - 1; i++)
            {
                for (var j = i + 1; j < cage.Length; j++)
                {
                    var twin = New(cage[i], cage[j]);
                    yield return twin[0];
                    yield return twin[1];
                }
            }
        }
    }

    public static ImmutableArray<Twin> New(Pos first, Pos second)
        => [new(first, second), new(second, first)];
}
