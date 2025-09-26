namespace SudokuSolver.Solvers;

public static partial class Solver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard, ReduceOptions.Default);

    public static Cells Solve(Clues clues, Rules rules, ReduceOptions? options = null)
    {
        var cells = Cells.Empty;
        var reduction = new Reduction(rules);

        var singles = Reduce(clues, cells, reduction, options ?? ReduceOptions.Default);
        var queue = Queue(singles, reduction);

        Backtrack(queue, cells);

        return cells;
    }

    private static ContextQueue Queue(PosSet done, Reduction reduction)
    {
        var q = new Constraint[(~done).Count];

        var count = 0;
        var min = 0;
        var max = 0;

        Rule[] rules = [.. reduction.Rules];
        max = rules.Length;

        while (count < q.Length)
        {
            var b_val = double.MinValue;
            var b_idx = 0;

            for (var idx = min; idx < max; idx++)
            {
                var rule = rules[idx];

                if (rule.Cells.IsSubsetOf(done))
                {
                    if (idx == max - 1)
                    {
                        max--;
                    }
                    else if (idx > min)
                    {
                        rules[idx++] = rules[min++];
                    }
                    continue;
                }
                var test = Score(rule);

                if (test > b_val)
                {
                    b_val = test;
                    b_idx = idx;
                }
            }
            Add(b_idx);
        }

        return new([.. q]);

        void Add(int indx)
        {
            var rule = rules[indx];

            var todo = rule.Cells;
            todo ^= done;

            foreach (var c in todo.Select(c => reduction[c]).OrderByDescending(r => r.Bits))
                q[count++] = c;

            done |= todo;

            if (indx != --max)
            {
                rules[indx] = rules[max];
            }
        }

        double Score(Rule rule)
        {
            var test = 0.0;
            
            foreach (var c in rule.Cells)
                test += done.Contains(c) ? 7 : reduction[c].Bits;

            test /= rule.Count;
            return test;
        }
    }
}
