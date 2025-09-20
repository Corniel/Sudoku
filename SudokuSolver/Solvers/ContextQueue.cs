using System.Runtime.CompilerServices;

namespace SudokuSolver.Solvers;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct ContextQueue(ImmutableArray<Constraint> constraints, int head = 0) : IReadOnlyCollection<Constraint>
{
    private readonly int Head = head;

    private readonly ImmutableArray<Constraint> Constraints = constraints;

    public bool IsEmpty => Head >= Constraints.Length;

    public int Count => Constraints.Length - Head;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Constraint Peek() => Constraints[Head];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ContextQueue Dequeue() => new(Constraints, Head + 1);

    public IEnumerator<Constraint> GetEnumerator() => Constraints.Skip(Head).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
