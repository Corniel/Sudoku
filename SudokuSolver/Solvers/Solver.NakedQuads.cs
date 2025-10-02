using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool NakedQuads(Context context)
    {
        foreach (var set in context.Rules.Where(r => r.IsSet && r.Count > 4))
        {
            var checks = set.Cells ^ context.Singles;

            foreach (var cell in checks)
                if (context[cell] is { Candidates.Count: 4 } ctx)
                    return Triple(ctx.Candidates, [ctx.Pos], checks ^ ctx.Pos);
        }

        return false;

        bool Triple(Candidates candidates, PosSet quad, PosSet checks)
        {
            foreach (var cell in checks)
            {
                var other = context[cell].Candidates;

                if (other.IsSubsetOf(candidates))
                {
                    quad |= cell;
                    if (quad.Count is 4)
                        return Remove(quad, candidates, checks, context);
                }
            }
            return false;
        }
    }
}
