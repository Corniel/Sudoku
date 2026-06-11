namespace DynamicSolver;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
[Mutable]
public sealed class StepTracer(int size = 1024) : IReadOnlyCollection<Step>
{
    private readonly Step[] Stack = new Step[size];

    /// <inheritdoc />
    public int Count { get; private set; }

    public bool Track(Links nodes, Pos cell, Digits mask)
    {
        var link = nodes[cell];
        var curr = link.Digits;
        var next = curr & mask;

        if (next == Digits.None)
        {
            link.Bits += Pars.Inconsistency;
            return false;
        }
        else if (next != curr)
        {
            link.Digits = next;
            Stack[Count++] = new(cell, curr);
        }
        return true;
    }

    public void Rollback(Links nodes)
    {
        while (Count > 0)
        {
            var step = Stack[--Count];
            nodes[step.Cell].Digits = step.Prev;
        }
    }

    public void Clear() => Count = 0;

    /// <inheritdoc />
    public IEnumerator<Step> GetEnumerator() => range(Count)
        .Select(i => Stack[Count - i - 1])
        .GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
