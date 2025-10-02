using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool NakedPairs(Context context)
    {
        var reduce = false;

        foreach (var set in context.Rules.Where(r => r.IsSet && r.Count > 2))
        {
            var checks = set.Cells ^ context.Singles;

            while (checks.HasAny)
            {
                var ctx = context[checks.First()];
                checks ^= ctx.Pos;
                if (ctx.Candidates.Count is 2)
                {
                    var pair = ctx.Candidates;
                    reduce |= Pair(ctx.Pos, pair, checks, set.Cells ^ context.Singles);
                }
            }
        }

        return reduce;

        bool Pair(Pos cell, Candidates pair, PosSet checks, PosSet cleans)
        {
            foreach (var other in checks)
            {
                if (context[other].Candidates == pair)
                    return Remove([cell, other], pair, cleans, context);
            }
            return false;
        }
    }
}
