using System.Numerics;

namespace SudokuSolver.Constraints;

/// <summary>Cells on a (chess) knight distance must have different digits.</summary>
public sealed class AntiKnight(Pos p1, Pos p2) : Constraint
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [p1, p2];

    public override ImmutableArray<Restriction> Restrictions => [];

    public static readonly ImmutableArray<AntiKnight> All = [.. Init()];

    private static IEnumerable<AntiKnight> Init()
    {
        for (Pos p1 = default; p1 < _9x9 - 1; p1++)
        {
            for (Pos p2 = p1 + 1; p2 < _9x9; p2++)
            {
                if (IsKnightDistance(p1, p2))
                {
                    yield return new AntiKnight(p1, p2);
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
