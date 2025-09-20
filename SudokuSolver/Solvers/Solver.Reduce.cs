namespace SudokuSolver.Solvers;

public static partial class Solver
{
    private static PosSet Reduce(Clues clues, Cells cells, Reduction rules)
    {
        var singles = PosSet.Empty;

        foreach (var (pos, val) in clues)
        {
            cells[pos] = val;
            rules[pos] = rules[pos].Solve(val);
            singles |= pos;
        }

        bool reduce;

        do
        {
            reduce = false;

            foreach (var pos in ~singles)
            {
                var ctx = rules[pos];
                var candidates = ctx.Candidates;
                var peers = ctx.Peers;

                foreach (var pr in ctx.Peers)
                {
                    var val = cells[pr];

                    if (val is not 0)
                    {
                        peers = peers.Remove(pr);
                        ctx = ctx with { Candidates = ctx.Candidates ^ val };
                    }
                }
                foreach (var res in ctx.Restrictions)
                {
                    ctx = ctx with { Candidates = ctx.Candidates & res.Restrict(cells) };
                }

                if (ctx.Candidates.HasSingle)
                {
                    cells[pos] = ctx.Candidates.First();
                    singles |= pos;
                    reduce = true;
                }
                else
                {
                    ctx = ctx with { Peers = peers };
                    reduce |= candidates != ctx.Candidates;
                }

                rules[ctx.Cell] = ctx;
            }
        }
        while (reduce);

        return singles;
    }
}
