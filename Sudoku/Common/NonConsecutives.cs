namespace Sudoku.Common;

public static class NonConsecutives
{
    public static IEnumerable<NonConsecutive> Orthogonally()
    {
        foreach (var pos in Pos.All)
        {
            if (pos.N() is { } n) yield return new NonConsecutive(pos, n);
            if (pos.W() is { } w) yield return new NonConsecutive(pos, w);
        }
    }
}
