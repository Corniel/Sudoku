namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    private static PosSet Reduce(Clues clues, Cells cells, CellContext[] contexts)
    {
        var singles = PosSet.Empty;

        foreach (var (pos, val) in clues)
        {
            cells[pos] = val;
            var ctx = contexts[pos];
            ctx.Candidates = [val];
            ctx.Peers = [];
            singles |= pos;
        }

        bool reduce;

        do
        {
            reduce = false;

            foreach (var pos in ~singles)
            {
                var ctx = contexts[pos];
                var candidates = ctx.Candidates;
                var peers = ctx.Peers;

                foreach (var pr in ctx.Peers)
                {
                    var val = cells[pr];

                    if (val is not 0)
                    {
                        peers = peers.Remove(pr);
                        ctx.Candidates ^= val;
                    }
                }
                foreach (var res in ctx.Restrictions)
                {
                    ctx.Candidates &= res.Restrict(cells);
                }

                if (ctx.Candidates.HasSingle)
                {
                    ctx.Peers = [];
                    cells[pos] = ctx.Candidates.First();
                    singles |= pos;
                    reduce = true;
                }
                else
                {
                    ctx.Peers = peers;
                    reduce |= candidates != ctx.Candidates;
                }
            }
        }
        while (reduce);

        return singles;
    }
}
