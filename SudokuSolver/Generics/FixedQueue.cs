namespace SudokuSolver.Generics;

/// <summary>A fixed queue.</summary>
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public struct FixedQueue<T>() : IReadOnlyCollection<T>
{
    private readonly T[] Queue = new T[_9x9];

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int Head;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int Tail;

    public readonly bool HasAny => Tail > Head;

    /// <inheritdoc />
    public readonly int Count => Tail - Head;

    /// <inheritdoc cref="Queue{T}.Dequeue()" />
    public T Dequeue() => Queue[Head++];

    /// <inheritdoc cref="Queue{T}.Enqueue(T)" />
    public void Enqueue(T item) => Queue[Tail++] = item;

    /// <inheritdoc cref="Queue{T}.Clear()" />
    public void Clear()
    {
        Head = 0;
        Tail = 0;
    }

    /// <inheritdoc />
    public readonly IEnumerator<T> GetEnumerator() => Queue.Skip(Head).Take(Count).GetEnumerator();

    /// <inheritdoc />
    readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
