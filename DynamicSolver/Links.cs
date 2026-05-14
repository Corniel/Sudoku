using Sudoku.Restrictions;
using System.Diagnostics.Contracts;

namespace DynamicSolver;

[Mutable]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public sealed class Links : IReadOnlyCollection<Link>, SudokuCells
{
    public static Links New(IEnumerable<Cell> clues, RuleSet rules)
    {
        var links = new Links();

        foreach (var pos in Pos.All)
            links.Lookup[pos] = new Link(pos);

        foreach (var set in rules.Sets)
            foreach (var peer in set)
                links[peer].Peers |= set ^ peer;

        foreach (var constraints in rules.Constraints)
            foreach (var cell in constraints.Cells)
            {
                var othr = links[cell];
                othr.Constraints.Add(constraints);
                othr.Bits += Pars.Constraints;
            }

        foreach (var restriction in rules.Restrictions)
        {
            if (restriction is Mask mask)
                links[mask.AppliesTo].Digits &= mask.Restrict(links);
            else
                foreach (var other in restriction.Cells ^ restriction.AppliesTo)
                {
                    var othr = links[other];
                    othr.Restrictions.Add(restriction);
                    othr.Bits += Pars.Restrictions;
                }
        }

        foreach (var set in rules.Sets)
            foreach (var peer in set)
                links[peer].Peers |= set ^ peer;

        foreach (var twins in rules.OfType<Twin>())
        {
            var (a, o) = (links[twins.AppliesTo], links[twins.Other]);
            a.Peers |= o.Peers ^ o.Pos;
            o.Peers |= a.Peers ^ a.Pos;
        }

        foreach (var (cell, value) in clues)
        {
            var link = links[cell];
            link.Digits = [value];
            links.Todos ^= cell;

            // Exclude clue from peers.
            foreach (var peer in link.Peers)
                links[peer].Digits ^= value;

            // Execute restrictions where clue is involved.
            foreach (var res in link.Restrictions)
                links[res.AppliesTo].Digits &= res.Restrict(links);
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

    [Pure]
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
