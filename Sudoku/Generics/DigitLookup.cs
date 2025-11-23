namespace Sudoku.Generics;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public sealed class DigitLookup<T> : IReadOnlyCollection<DigitLookup<T>.LookupValue>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T[] lookup = new T[(Digits.Mask >> 1) + 1];

    /// <inheritdoc />
    public int Count => lookup.Length - 1;

    public T this[Digits digits]
    {
        get => lookup[digits.Bits >> 1];
        set => lookup[digits.Bits >> 1] = value;
    }

    /// <inheritdoc />
    public IEnumerator<LookupValue> GetEnumerator()
        => Digits.All.Skip(1)
        .Select(d => new LookupValue(d, this[d]))
        .GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public readonly record struct LookupValue(Digits Key, T Value)
    {
        /// <inheritdoc />
        public override string ToString() => $"{Key} = {Value}";
    }
}
