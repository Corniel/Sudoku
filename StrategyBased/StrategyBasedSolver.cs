namespace StrategyBased;

public sealed class StrategyBasedSolver(Nodes nodes, ReduceOptions options) : IEnumerator<SolveResult>, IEnumerable<SolveResult>
{
    public StrategyBasedSolver(Clues clues, Rules rules, ReduceOptions options)
        : this(Nodes.Empty & rules & clues, options) { }

    public static Cells Solve(Clues clues) => Solve(clues, Rules.Standard, ReduceOptions.All);

    public static Cells Solve(Clues clues, Rules rules, ReduceOptions? options = null)
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
        foreach (var strategy in Options.Strategies)
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
