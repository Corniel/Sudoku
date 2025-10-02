using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

public static partial class Solver
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool NakedSingles(Cells cells, Context context)
    {
        var reduce = false;

        foreach (var pos in context.Todos)
        {
            var ctx = context[pos];
            var candidates = ctx.Candidates;

            if (candidates.HasSingle)
            {
                reduce |= Remove(pos, candidates.First(), cells, context);
            }
        }
        return reduce;
    }
}
