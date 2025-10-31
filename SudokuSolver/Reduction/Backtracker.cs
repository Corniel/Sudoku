namespace Sudoku.Reduction;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Backtracker(Graph graph, ImmutableArray<Node> nodes, int head = 0) : IReadOnlyCollection<Node>
{
    public static Backtracker New(Graph graph, bool log)
    {
        var queue = new Node[graph.Todo.Count];
        var todos = graph.Todo;
        var count = 0;
        var score = new double[_9x9];

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
                {
                    var sc = (double)node.Digits.Count * node.Peers.Count / node.Links.Count;
                    score[node.Pos] = sc;
                    test *= sc;
                }

                if (test < best)
                {
                    best = test;
                    hous = cs;
                }
            }

#if DEBUG
            if (log) Console.WriteLine($"House = {hous.Count}, F = {hous.Select(p => score[p]).Product():0.0}");
#endif
            foreach (var cell in hous.OrderBy(c => graph[c].Digits.Count))
            {
#if DEBUG
                if (log) Console.WriteLine($"{cell}, Digits = {graph[cell].Digits.Count}, Links {graph[cell].Links.Count}, F = {score[cell]:0.000}");
#endif
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

        var digits = node.Digits;

        foreach (var peer in node.Backgtracking)
            digits ^= Graph[peer].Digit;

        foreach (var restriction in node.Restrictions)
            digits &= restriction.Restrict(Graph);

        foreach (var digit in digits)
        {
            node.Test(digit);

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
