namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    private static bool Solve(ContextQueue queue, Cells cells)
    {
        if (queue.IsEmpty) return true;

        var ctx = queue.Peek();
        var candidates = ctx.Candidates;

        foreach (var peer in ctx.Peers)
        {
            candidates ^= cells[peer];
        }

        foreach (var res in ctx.Restrictions)
        {
            candidates &= res.Restrict(cells);
        }

        foreach (var candidate in candidates)
        {
            cells[ctx.Pos] = candidate;

            if (Solve(queue.Dequeue(), cells))
            {
                return true;
            }
        }
        cells[ctx.Pos] = 0;

        return false;
    }
}
