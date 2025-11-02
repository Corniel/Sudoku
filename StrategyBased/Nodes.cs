using Sudoku.Houses;
using Sudoku.Restrictions;

namespace StrategyBased;

[Mutable]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}, Version = {Version}, Todo = {Todo.Count}")]
public sealed class Nodes : IReadOnlyCollection<Node>, SudokuCells
{
    public static Nodes Empty => new(new());

    private Nodes(Root root)
    {
        Root = root;
        var nodes = Root.Nodes;

        for (Pos cell = Pos.O; cell < nodes.Length; cell++)
            nodes[cell] = new Node(cell, root);
    }

    /// <inheritdoc />
    public int Count => Root.Nodes.Length;

    public int Version => Root.Version;

    public PosSet Todo => Root.Todo;

    public PosSet Restricted { get; private set; }

    public Rules Rules { get; set; } = Rules.None;

    /// <summary>Gets all houses (e.a. sets with size 9).</summary>
    public ImmutableArray<Rule> Houses { get; set; } = [];

    /// <summary>Gets all rows.</summary>
    public ImmutableArray<Row> Rows { get; set; } = [];

    /// <summary>Gets all cols.</summary>
    public ImmutableArray<Col> Cols { get; set; } = [];

    public Node this[Pos pos] => Root.Nodes[pos];

    SudokuCell SudokuCells.this[Pos pos] => Root.Nodes[pos];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool DoesNotOccur(int value, PosSet cells)
        => cells.NotAny(cell => Root.Nodes[cell].Digits.Contains(value));

    public PosSet[] Assignments(PosSet set)
    {
        var assignments = new PosSet[_9 + 1];

        foreach (var cell in set & Todo)
            foreach (var value in this[cell].Digits)
                assignments[value] |= cell;

        return assignments;
    }

    public IEnumerable<CandidatesCells> NakedCells(ImmutableArray<Pos> set, int size)
    {
        var start = 0;

        while (start <= set.Length - size)
        {
            var i = start++;

            var node = this[set[i++]];
            var digits = node.Digits;

            if (digits.Count > size) continue;

            var cells = PosSet.New(node.Pos);

            while (i < set.Length)
            {
                var next = this[set[i++]];

                var test = digits | next.Digits;

                if (test.Count > size) continue;

                digits = test;
                cells |= next.Pos;

                if (digits.Count == size && cells.Count == size)
                {
                    yield return new(digits, cells);
                }
            }
        }
    }

    public string Log()
    {
        var sb = new StringBuilder();

        for (var row = 0; row < _9; row++)
        {
            sb.AppendLine(Line(row));

            for (var offset = 0; offset < _9; offset += 3)
            {
                sb.Append('│');
                for (var col = 0; col < _9; col++)
                {
                    var node = this[(row, col)];
                    Block(node, offset);
                    sb.Append(col is 2 or 5 ? '║' : '│');
                }
                sb.Append('\n');
            }
        }
        sb.Append(Line(-1));

        return sb.ToString();

        void Block(Node node, int offset)
        {
            if (node.Digit is not 0)
            {
                sb.Append(offset is 3
                    ? $" ({node.Digit}) "
                    : "     ");
            }
            else
            {
                for (var val = offset + 1; val <= offset + 3; val++)
                {
                    sb.Append(node.Digits.Contains(val) ? val : ".");

                    if (val - offset is 1 or 2) sb.Append(' ');
                }
            }
        }

        static string Line(int row) => row switch
        {
            000000 => "┌─────┬─────┬─────╥─────┬─────┬─────╥─────┬─────┬─────┐",

            3 or 6 => "╞═════╪═════╪═════╬═════╪═════╪═════╬═════╪═════╪═════╡",

            -00001 => "└─────┴─────┴─────╨─────┴─────┴─────╨─────┴─────┴─────┘",

            _/*.*/ => "├─────┼─────┼─────╫─────┼─────┼─────╫─────┼─────┼─────┤",
        };
    }

    public static Nodes operator &(Nodes graph, Rules rules)
    {
        graph.Houses = graph.Houses.AddRange(rules.Where(r => r.IsHouse));
        graph.Rows = graph.Rows.AddRange(rules.OfType<Row>());
        graph.Cols = graph.Cols.AddRange(rules.OfType<Col>());

        foreach (var rule in rules.OrderByDescending(r => r.Count))
        {
            graph.Rules += rule;

            foreach (var cell in rule.Cells)
                graph[cell].Links |= rule.Cells;

            if (rule.IsSet)
            {
                foreach (var cell in rule.Cells)
                    graph[cell].Peers |= rule.Cells;
            }
        }

        foreach (var restriction in rules.Restrictions)
        {
            if (restriction is Mask mask)
            {
                graph[restriction.AppliesTo].Digits &= mask.Restrict(graph);
            }
            else
            {
                graph.Restricted |= restriction.AppliesTo;
                graph[restriction.AppliesTo].Restrictions.Add(restriction);
                graph[restriction.AppliesTo].Links |= restriction.Links;

                if (restriction is Pair pair)
                {
                    var paired = graph[pair.AppliesTo].PairedRestrictions;
                    paired.TryAdd(pair.Other, []);
                    paired[pair.Other].Add(pair);
                }
            }
        }

        return graph;
    }

    public static Nodes operator &(Nodes graph, Clues clues)
    {
        foreach (var clue in clues)
            graph[clue.Pos].Digits = [clue.Digit];

        return graph;
    }

    public static bool operator &(Nodes graph, Action<Nodes> reduce)
    {
        if (graph.Todo.HasNone) return false;
        var version = graph.Version;
        reduce(graph);
        return version != graph.Version;
    }

    /// <inheritdoc />
    public IEnumerator<Node> GetEnumerator() => ((IEnumerable<Node>)Root.Nodes).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly Root Root;
}
