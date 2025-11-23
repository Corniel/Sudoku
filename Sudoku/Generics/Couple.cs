namespace Sudoku.Generics;

[DebuggerDisplay("Count = 2")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct Couple<T>(T one, T two) : IReadOnlyCollection<T>
{
    public readonly T One = one;
    public readonly T Two = two;

    /// <inheritdoc />
    public int Count => 2;

    /// <inheritdoc />
    [Pure]
    public IEnumerator<T> GetEnumerator()
    {
        yield return One;
        yield return Two;
    }

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
