namespace Sudoku;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly partial struct Rules(ImmutableArray<Rule> rules, ImmutableArray<Restriction> restrictions) : IReadOnlyCollection<Rule>
{
    public IEnumerable<PosSet> Sets => Collection.Where(x => x.IsSet).Select(x => x.Cells);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<Rule> Collection = rules;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public readonly ImmutableArray<Restriction> Restrictions = restrictions;

    /// <inheritdoc />
    public int Count => Collection.Length;

    /// <inheritdoc />
    public IEnumerator<Rule> GetEnumerator() => ((IEnumerable<Rule>)Collection).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Rules operator +(Rules rules, Rule rule) => rules + [rule];

    public static Rules operator +(Rules rules, IEnumerable<Rule> add) => new(
        rules.Collection.AddRange(add),
        rules.Restrictions.AddRange(add.SelectMany(r => r.Restrictions)));

    public static Rules operator +(Rules rules, IEnumerable<Restriction> add) => new(
        rules.Collection,
        rules.Restrictions.AddRange(add));
}
