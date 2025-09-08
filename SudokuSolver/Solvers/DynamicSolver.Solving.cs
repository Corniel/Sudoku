using SudokuSolver.Generics;

namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    private static Cells Solve(Clues clues, CellContext[] contexts, Rules rules)
    {
        var cells = Cells.Empty;

        var singles = ProcessGivens(clues, cells, contexts);
        singles |= ResolveNakedSingles(cells, contexts, ~singles);

        foreach (var res in rules.Restrictions)
        {
            var ctx = contexts[res.AppliesTo];
            ctx.Candidates &= res.Restrict(cells);

            if (ctx.Candidates.HasSingle && !singles.Contains(ctx.Pos))
            {
                singles |= ResolveSingle(ctx, ctx.Candidates, cells, contexts);
            }
        }

        var prio = new CellContext[_9x9];
        var len = 0;
        var done = singles;
        var queue = new FixedQueue<CellContext>();
        var first = contexts.Where(c => !done.Contains(c.Pos)).OrderBy(c => c.Candidates.Count).First();
        queue.Enqueue(first);
        done |= first.Pos;

        while (queue.HasAny)
        {
            var ctx = queue.Dequeue();
            prio[len++] = ctx;

            foreach (var peer in ctx.Peers.OrderBy(p => contexts[p].Peers.Length))
            {
                if (done.Contains(peer)) continue;
                done |= peer;
                queue.Enqueue(contexts[peer]);
            }
        }

        Solve(new([.. prio[..len]]), cells);

        var solved = Cells.Empty;

        foreach (var p in Pos.All) solved[p] = cells[p];

        return solved;
    }

    /// <summary>Proccesses the given cells.</summary>
    /// <remarks>
    /// for all given cells:
    /// * remove its value from the candidates of its peers
    /// * remove itself for its peers
    /// * update its candidates to this single value
    /// * remove all its peers
    /// * add the cell to to the singles.
    /// </remarks>
    private static PosSet ProcessGivens(Clues clues, Cells cells, CellContext[] squares)
    {
        var solved = PosSet.Empty;

        foreach (var clue in clues)
        {
            solved |= ResolveSingle(squares[clue.Pos], Candidates.New(clue.Value), cells, squares);
        }
        return solved;
    }

    private static PosSet ResolveNakedSingles(Cells cells, CellContext[] contexts, PosSet unsolved)
    {
        var solved = PosSet.Empty;

        foreach (var p in unsolved)
        {
            var ctx = contexts[p];

            if (ctx.Candidates.HasSingle)
            {
                solved |= ResolveSingle(ctx, ctx.Candidates, cells, contexts);
            }
        }
        return solved;
    }

    private static Pos ResolveSingle(CellContext ctx, Candidates value, Cells cells, CellContext[] contexts)
    {
        foreach (var lin in ctx.Peers)
        {
            var peer = contexts[lin];

            if (peer.Candidates.HasSingle) continue;

            peer.Candidates ^= value;
            peer.Peers = peer.Peers.Remove(ctx.Pos);
        }
        ctx.Peers = [];
        ctx.Candidates = value;
        cells[ctx.Pos] = value.First();

        return ctx.Pos;
    }
}
