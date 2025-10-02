using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool NakedTriples(Context context)
    {
        foreach (var set in context.Rules.Where(r => r.IsSet && r.Count > 3))
        {
            var checks = set.Cells ^ context.Singles;

            foreach (var cell in checks)
                if (context[cell] is { Candidates.Count: 3 } ctx)
                    return Triple(ctx.Candidates,[ctx.Pos], checks ^ ctx.Pos);
        }

        return false;

        bool Triple(Candidates candidates, PosSet triple, PosSet checks)
        {
            foreach (var cell in checks)
            {
                var other = context[cell].Candidates;

                if (other.IsSubsetOf(candidates))
                {
                    triple |= cell;
                    if (triple.Count is 3)
                        return Remove(triple, candidates, checks, context);
                }
            }
            return false;
        }
    }

}
