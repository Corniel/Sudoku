using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct ContextQueue(ImmutableArray<CellContext> contexts, int head = 0) : IReadOnlyCollection<CellContext>
{
    private readonly int Head = head;

    private readonly ImmutableArray<CellContext> Contexts = contexts;

    public bool IsEmpty => Head >= Contexts.Length;

    public int Count => Contexts.Length - Head;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public CellContext Peek() => Contexts[Head];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ContextQueue Dequeue() => new(Contexts, Head + 1);

    public IEnumerator<CellContext> GetEnumerator() => Contexts.Skip(Head).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
