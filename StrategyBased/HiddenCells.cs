using System.Numerics;

namespace StrategyBased;

public readonly record struct HiddenCells
{
    public required int Digit { get; init; }

    /// <summary>Index of the linked house.</summary>
    public required int Index { get; init; }

    public required Indexes Indexes { get; init; }

    public required PosSet Cells { get; init; }

    public required PosSet Peers { get; init; }
}

public readonly struct Indexes(uint bits) : IEquatable<Indexes>, IReadOnlyCollection<int>
{
    public static readonly Indexes None;

    private readonly uint Bits = bits;

    /// <inheritdoc />
    public int Count => BitOperations.PopCount(Bits);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int First() => BitOperations.TrailingZeroCount(Bits);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Last() => BitOperations.Log2(Bits);


    public override bool Equals(object? obj) => obj is Indexes other && Equals(other);

    public bool Equals(Indexes other) => Bits == other.Bits;

    public override int GetHashCode() => (int)Bits;

    public override string ToString() => string.Join(", ", this);

    public static bool operator ==(Indexes l, Indexes r) => l.Equals(r);

    public static bool operator !=(Indexes l, Indexes r) => !(l == r);

    public static Indexes operator |(Indexes vals, int val) => new(vals.Bits | (1U << val));

    public static Indexes operator &(Indexes l, Indexes r) => new(l.Bits & r.Bits);

    public static Indexes operator |(Indexes l, Indexes r) => new(l.Bits | r.Bits);

    public static Indexes operator ^(Indexes l, Indexes r) => new(l.Bits & ~r.Bits);

    public static Indexes operator ^(Indexes indexes, int index) => new(indexes.Bits & ~(1U << index));

    public Iterator GetEnumerator() => new(Bits);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

    public struct Iterator(uint bits) : IEnumerator<int>, IEnumerable<int>
    {
        private uint Remaining = bits;

        public int Current { get; private set; } = -1;

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Remaining is 0) return false;

            var trailing = BitOperations.TrailingZeroCount(Remaining) + 1;
            Remaining >>= trailing;

            Current += trailing;
            return true;
        }

        public readonly IEnumerator<int> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();

        public readonly void Dispose() { /* Nothing to dispose. */ }
    }
}
