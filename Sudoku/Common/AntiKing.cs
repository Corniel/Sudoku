namespace Sudoku.Common;

/// <summary>Cells on a (chess) knight distance must have different digits.</summary>
public sealed class AntiKing(Pos p1, Pos p2) : Set(p1, p2)
{
    public override ImmutableArray<Restriction> Restrictions => [];

    public static readonly ImmutableArray<AntiKing> All = [.. Init()];

    private static IEnumerable<AntiKing> Init()
    {
        for (Pos p1 = default; p1 < _9x9 - 1; p1++)
        {
            for (Pos p2 = p1 + 1; p2 < _9x9; p2++)
            {
                if (IsKingDistance(p1, p2))
                {
                    yield return new AntiKing(p1, p2);
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
}
