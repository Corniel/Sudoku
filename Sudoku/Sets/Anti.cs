using System.Collections.Frozen;

namespace Sudoku.Sets;

public static class Anti
{
    /// <summary>Cells on a chess king's distance away have different digits.</summary>
    public static readonly FrozenSet<Rule> King = [.. Kings()];

    /// <summary>Cells on a chess knight's distance away have different digits.</summary>
    public static readonly FrozenSet<Rule> Knight = [.. Knights()];

    private static IEnumerable<CellSet> Kings()
    {
        for (Pos p1 = default; p1 < _9x9 - 1; p1++)
        {
            for (Pos p2 = p1 + 1; p2 < _9x9; p2++)
            {
                if (IsKingDistance(p1, p2))
                {
                    yield return new CellSet([p1, p2], "Anti King");
                }
            }
        }

        static bool IsKingDistance(Pos p1, Pos p2)
        {
            var (r1, c1) = p1;
            var (r2, c2) = p2;
            return ((r1 - r2).Sqr() + (c1 - c2).Sqr()) is 1 or 2;
        }
    }

    private static IEnumerable<CellSet> Knights()
    {
        for (Pos p1 = default; p1 < _9x9 - 1; p1++)
        {
            for (Pos p2 = p1 + 1; p2 < _9x9; p2++)
            {
                if (IsKnightDistance(p1, p2))
                {
                    yield return new CellSet([p1, p2], "Anti Knight");
                }
            }
        }

        static bool IsKnightDistance(Pos p1, Pos p2)
        {
            var (r1, c1) = p1;
            var (r2, c2) = p2;
            return (r1 - r2).Sqr() + (c1 - c2).Sqr() == 5;
        }
    }
}
