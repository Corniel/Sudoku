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

    public static Nodes operator &(Nodes nodes, Rules rules)
    {
        nodes.Houses = nodes.Houses.AddRange(rules.Where(r => r.IsHouse));
        nodes.Rows = nodes.Rows.AddRange(rules.OfType<Row>());
        nodes.Cols = nodes.Cols.AddRange(rules.OfType<Col>());

        foreach (var rule in rules.OrderByDescending(r => r.Count))
        {
            nodes.Rules += rule;

            foreach (var cell in rule.Cells)
                nodes[cell].Links |= rule.Cells;

            if (rule.IsSet)
            {
                foreach (var cell in rule.Cells)
                    nodes[cell].Peers |= rule.Cells;
            }
        }
        foreach (var node in nodes)
        {
            node.Peers ^= node.Pos;
            node.Links ^= node.Pos;
        }

        foreach (var restriction in rules.Restrictions)
        {
            if (restriction is Mask mask)
            {
                nodes[restriction.AppliesTo].Digits &= mask.Restrict(nodes);
            }
            else
            {
                nodes.Restricted |= restriction.AppliesTo;
                nodes[restriction.AppliesTo].Restrictions.Add(restriction);
                nodes[restriction.AppliesTo].Links |= restriction.Links;

                if (restriction is Pair pair)
                {
                    var paired = nodes[pair.AppliesTo].PairedRestrictions;
                    paired.TryAdd(pair.Other, []);
                    paired[pair.Other].Add(pair);
                }
            }
        }

        return nodes;
    }

    public static Nodes operator &(Nodes nodes, Clues clues)
    {
        foreach (var clue in clues)
            nodes[clue.Pos].Digits = [clue.Digit];

        return nodes;
    }

    public static bool operator &(Nodes nodes, Reduce reduce)
    {
        if (nodes.Todo.HasNone) return false;
        var version = nodes.Version;
        reduce(nodes);
        return version != nodes.Version;
    }

    /// <inheritdoc />
    public IEnumerator<Node> GetEnumerator() => ((IEnumerable<Node>)Root.Nodes).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private readonly Root Root;
}
