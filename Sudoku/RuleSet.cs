namespace Sudoku;

/// <summary>A set of rules that apply when solving a set of <see cref="Clues"/>.</summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly partial struct RuleSet(ImmutableArray<Rule> rules) : IReadOnlyCollection<Rule>
{
    public IEnumerable<Constraint> Constraints => Rules.OfType<Constraint>();

    public IEnumerable<Restriction> Restrictions => Rules.OfType<Restriction>();

    public IEnumerable<PosSet> Sets => Rules.OfType<Set>().Select(x => x.Cells);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly ImmutableArray<Rule> Rules = rules;

    /// <inheritdoc />
    public int Count => Rules.Length;

    /// <inheritdoc />
    public IEnumerator<Rule> GetEnumerator() => Rules.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static RuleSet operator +(RuleSet ruleSet, Rule rule)
        => new(ruleSet.Rules.Add(rule));

    public static RuleSet operator +(RuleSet ruleSet, Rules rules)
        => new(ruleSet.Rules.AddRange(rules));

    public static RuleSet operator +(RuleSet rules, RulesExtender extender) => extender(rules);
}
