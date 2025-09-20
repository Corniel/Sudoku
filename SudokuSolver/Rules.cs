namespace SudokuSolver;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly partial struct Rules(ImmutableArray<Rule> rules, Constraint[] cells) : IReadOnlyCollection<Rule>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<Rule> Collection = rules;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Constraint[] Cells = cells;

    /// <inheritdoc />
    public int Count => Collection.Length;

    public Constraint this[Pos pos] => Cells[pos];

    public IReadOnlyList<Constraint> Cons => Cells;

    /// <inheritdoc />
    public IEnumerator<Rule> GetEnumerator() => ((IEnumerable<Rule>)Collection).GetEnumerator();

    public Constraint[] ToArray()
    {
        var copy = new Constraint[Cells.Length];
        Array.Copy(Cells, copy, copy.Length);
        return copy;
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Rules operator +(Rules rules, Rule rule) => rules + [rule];

    public static Rules operator +(Rules rules, IEnumerable<Rule> add)
    {
        var copy = rules.ToArray();
        var all = rules.Collection;

        foreach (var rule in add)
        {
            if (rule.Restrictions.Length > 0
                || !rules.Collection.Any(r => rule.Cells.IsSubsetOf(r.Cells)))
            {
                all = all.Add(rule);
            }

            if (rule.IsSet)
            {
                foreach (var peer in rule.Cells)
                {
                    copy[peer] += rule.Cells;
                }
            }
            else
            {
                all = all.Add(rule);
            }
            foreach (var res in rule.Restrictions)
            {
                copy[res.AppliesTo] += res;
            }
        }
        return new(all, copy);
    }
}
