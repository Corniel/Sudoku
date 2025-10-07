namespace SudokuSolver.Reduction;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Backtracker(Graph graph, ImmutableArray<Node> nodes, int head = 0) : IReadOnlyCollection<Node>
{
    public static Backtracker New(Graph graph)
    {
        var queue = new Node[graph.Todo.Count];
        var todos = graph.Todo;
        var count = 0;

        PosSet[] houses = [..graph.Houses.Select(h => h.Cells & graph.Todo).OrderBy(h => h.Count)];

        var min = 0;
        var max = houses.Length;

        while (todos.HasAny)
        {
            var best = double.MaxValue;
            var hous = houses[min] & todos;

            for (var i = min; i < max; i++)
            {
                var house = houses[i];

                var test = 1d;

                var cs = house & todos;

                if (cs.Count is 0)
                {
                    if (i == min)
                    {
                        min++;
                    }
                    else if (i == max - 1)
                    {
                        max--;
                    }
                    continue;
                }

                foreach (var node in cs.Select(c => graph[c]))
                    test *= (double)node.Candidates.Count * node.Peers.Count / node.Links.Count;

                if (test < best)
                {
                    best = test;
                    hous = cs;
                }
            }

            foreach (var cell in hous.OrderBy(c => graph[c].Candidates.Count))
            {
                todos ^= cell;
                queue[count++] = graph[cell].Freeze(todos);
            }
        }

        return new(graph, [.. queue]);
    }

    private readonly int Head = head;

    private readonly Graph Graph = graph;

    private readonly ImmutableArray<Node> Nodes = nodes;

    public bool IsEmpty => Head >= Nodes.Length;

    public int Count => Nodes.Length - Head;

    public bool Solve()
    {
        if (IsEmpty) return true;

        var node = Peek();

        var candidates = node.Candidates;

        foreach (var peer in node.Backgtracking)
            candidates ^= Graph[peer].Value;

        foreach (var restriction in node.Restrictions)
            candidates &= restriction.Restrict(Graph);

        foreach (var candidate in candidates)
        {
            node.Test(candidate);

            if (Dequeue().Solve())
            {
                return true;
            }
        }

        node.Reset();

        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Node Peek() => Nodes[Head];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Backtracker Dequeue() => new(Graph, Nodes, Head + 1);

    public IEnumerator<Node> GetEnumerator() => Nodes.Skip(Head).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
