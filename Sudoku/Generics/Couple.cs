namespace Sudoku.Generics;

[DebuggerDisplay("Count = 2")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct Couple<T>(T one, T two) : IReadOnlyCollection<T>
{
    public readonly T One = one;
    public readonly T Two = two;

    /// <inheritdoc />
    public int Count => 2;

    /// <summary>Deconstructs the couple.</summary>
    public void Deconstruct(out T one, out T two) => (one, two) = (One, Two);

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
