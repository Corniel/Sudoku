namespace SudokuSolver.Solvers;

public static partial class Solver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard);

    public static Cells Solve(Clues clues, Rules rules)
    {
        var cells = Cells.Empty;
        var reduction = new Reduction(rules);

        var singles = Reduce(clues, cells, reduction);
        var queue = Queue(singles, reduction);

        Solve(queue, cells);

        return cells;
    }

    private static ContextQueue Queue(PosSet done, Reduction reduction)
    {
        var unsolved = ~done;
        var q = new Constraint[unsolved.Count];

        var count = 0;
        var size = 0;

        var groups = new PosSet[reduction.Rules.Count];

        foreach (var rule in reduction.Rules)
            groups[size++] = rule.Cells;

        // every loop, we try to find set with adds a minimum of new combiations
        while (count < q.Length)
        {
            var best = int.MaxValue;
            var index = int.MaxValue;
            var group = unsolved;

            for (var i = 0; i < size; i++)
            {
                var set = groups[i] & unsolved;

                while (set.HasNone && i > 0 && i < size - 1)
                {
                    set = groups[--size] & unsolved;
                    groups[i] = set;
                }

                var test = 1;

                foreach (var c in set)
                {
                    test *= reduction[c].Candidates.Count;

                    // looping sets is more expensive than branching
                    if (test > best) break;
                }

                if (test < best)
                {
                    group = set;
                    best = test;
                    index = i;
                }
            }

            foreach (var p in group.OrderBy(p => reduction[p].Candidates.Count))
                q[count++] = reduction[p];

            if (size > 0)
            {
                groups[index] = groups[--size];
                unsolved ^= group;
            }
        }

        return new([.. q]);
    }
}
