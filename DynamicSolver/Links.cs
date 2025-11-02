namespace DynamicSolver;

[Mutable]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public sealed class Links : IReadOnlyCollection<Link>, SudokuCells
{
    public static Links New(Clues clues, Rules rules)
    {
        var links = new Links();

        foreach (var pos in Pos.All)
            links.Lookup[pos] = new Link(pos);

        foreach (var set in rules.Sets)
            foreach (var peer in set)
                links[peer].Peers |= set ^ peer;

        foreach (var restriction in rules.Restrictions)
            foreach (var other in restriction.Links)
                links[other].Restrictions.Add(restriction);

        foreach (var (cell, value) in clues)
        {
            var node = links[cell];
            node.Digits = [value];
            links.Todos ^= cell;

            foreach (var peer in node.Peers)
                links[peer].Digits ^= value;
        }

        return links;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Link[] Lookup = new Link[_9x9];

    public Link this[Pos pos] => Lookup[pos];

    /// <inheritdoc />
    SudokuCell SudokuCells.this[Pos pos] => Lookup[pos];

    /// <inheritdoc />
    public int Count => Lookup.Length;

    public PosSet Todos { get; private set; } = PosSet.All;

    /// <inheritdoc />
    public IEnumerator<Link> GetEnumerator() => ((IReadOnlyCollection<Link>)Lookup).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

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

        void Block(Link link, int offset)
        {
            if (link.Digits.HasSingle)
            {
                sb.Append(offset is 3
                    ? $" ({link.Digit}) "
                    : "     ");
            }
            else
            {
                for (var digit = offset + 1; digit <= offset + 3; digit++)
                {
                    sb.Append(link.Digits.Contains(digit) ? digit : ".");

                    if (digit - offset is 1 or 2) sb.Append(' ');
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
}
