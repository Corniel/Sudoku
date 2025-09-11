namespace SudokuSolver.Solvers;

public static partial class DynamicSolver
{
    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard);

    public static Cells Solve(Clues clues, ImmutableArray<Constraint> rules)
    {
        var peers = range(_9x9, _ => PosSet.Empty);
        var restrictions = range(_9x9, _ => new List<Restriction>());

        foreach (var @set in rules.Where(r => r.IsSet))
            foreach (var peer in @set)
                peers[peer] |= set.Cells;

        foreach (var constraint in rules)
            foreach (var res in constraint.Restrictions)
                restrictions[res.AppliesTo].Add(res);

        CellContext[] contexts = [.. Pos.All.Select(p => new CellContext(p)
        {
            Peers = [.. peers[p] ^ p],
            Restrictions = [.. restrictions[p]],
        })];

        var cells = Cells.Empty;

        var singles = Reduce(clues, cells, contexts);
        var queue = Queue(singles, contexts, rules);

        Solve(queue, cells);

        return cells;
    }

    private static ContextQueue Queue(PosSet done, CellContext[] contexts, ImmutableArray<Constraint> rules)
    {
        var unsolved = ~done;
        var q = new CellContext[unsolved.Count];

        var count = 0;
        var size = 0;

        var groups = new PosSet[rules.Length];

        foreach (var rule in rules)
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

                while (set.HasNone && i < size + 1)
                {
                    set = groups[--size] & unsolved;
                    groups[i] = set;
                }

                var test = 1;

                foreach (var c in set)
                {
                    test *= contexts[c].Candidates.Count;

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

            foreach (var p in group.OrderBy(p => contexts[p].Candidates.Count))
                q[count++] = contexts[p];

            if (size > 0)
            {
                groups[index] = groups[--size];
                unsolved ^= group;
            }
        }

        return new([.. q]);
    }

    private static T[] range<T>(int size, Func<int, T> select) => [.. Enumerable.Range(0, size).Select(select)];
}
