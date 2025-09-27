using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Backtracker(ImmutableArray<Constraint> constraints, int head = 0) : IReadOnlyCollection<Constraint>
{
    private readonly int Head = head;

    private readonly ImmutableArray<Constraint> Constraints = constraints;

    public bool IsEmpty => Head >= Constraints.Length;

    public int Count => Constraints.Length - Head;

    public bool Solve(Cells cells)
    {
        if (IsEmpty) return true;

        var ctx = Peek();
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
            cells[ctx.Cell] = candidate;

            if (Dequeue().Solve(cells))
            {
                return true;
            }
        }
        cells[ctx.Cell] = 0;

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Constraint Peek() => Constraints[Head];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Backtracker Dequeue() => new(Constraints, Head + 1);

    public IEnumerator<Constraint> GetEnumerator() => Constraints.Skip(Head).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Backtracker Init(Context context)
    {
        var q = new Constraint[context.Todos.Count];

        var count = 0;
        var min = 0;
        var max = 0;

        Rule[] rules = [.. context.Rules];
        max = rules.Length;

        while (count < q.Length)
        {
            var b_val = double.MinValue;
            var b_idx = 0;

            for (var idx = min; idx < max; idx++)
            {
                var rule = rules[idx];

                if (rule.Cells.IsSubsetOf(context.Singles))
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
            todo ^= context.Singles;

            foreach (var c in todo.Select(c => context[c].Constraint).OrderByDescending(r => r.Bits))
                q[count++] = c;

            context.Singles |= todo;

            if (indx != --max)
            {
                rules[indx] = rules[max];
            }
        }

        double Score(Rule rule)
        {
            var test = 0.0;

            foreach (var c in rule.Cells)
                test += context.Singles.Contains(c) ? 7 : context[c].Constraint.Bits;

            test /= rule.Count;
            return test;
        }
    }
}
