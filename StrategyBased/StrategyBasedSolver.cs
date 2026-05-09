namespace StrategyBased;

public sealed class StrategyBasedSolver(Nodes nodes, ReduceOptions options) : IEnumerator<SolveResult>, IEnumerable<SolveResult>
{
    public StrategyBasedSolver(Clues clues, RuleSet rules, ReduceOptions options)
        : this(Nodes.Empty & rules & clues, options) { }

    public static Cells Solve(Clues clues) => Solve(clues, RuleSet.Standard, ReduceOptions.All);

    public static Cells Solve(Clues clues, RuleSet rules, ReduceOptions? options = null)
    {
        var solver = new StrategyBasedSolver(Nodes.Empty & rules & clues, options ?? ReduceOptions.All);
        _ = solver.LastOrDefault();
        return Cells.New(solver.Nodes);
    }

    public Nodes Nodes { get; } = nodes;
    private readonly ReduceOptions Options = options;

    public SolveResult Current { get; private set; }

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        var reduced = false;
        var first = Options.Strategies[0];

        // Merge attempts on the first strategy.
        while (Nodes & first.Reduce)
        {
            Current = new SolveResult(first.Type, Nodes);
            reduced = true;
        }

        if (reduced) return true;

        foreach (var strategy in Options.Strategies.Skip(1))
        {
            if (Nodes & strategy.Reduce)
            {
                Current = new SolveResult(strategy.Type, Nodes);
                return true;
            }
        }
#if DEBUG
        if (Options.Log) Console.WriteLine(Nodes.Log());
#endif
        return false;
    }

    void IDisposable.Dispose() { /* Nothging to dispose */ }

    void IEnumerator.Reset() => throw new NotSupportedException();

    public IEnumerator<SolveResult> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;
}
