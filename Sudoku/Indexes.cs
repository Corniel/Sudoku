using System.Numerics;

namespace Sudoku;

public readonly struct Indexes(uint bits) : IEquatable<Indexes>, IReadOnlyCollection<int>
{
    public static readonly Indexes None;

    public static readonly Indexes _0_8 = new(0b111_111_111);

    private readonly uint Bits = bits;

    /// <inheritdoc />
    public int Count => BitOperations.PopCount(Bits);

    public bool HasNone => Bits == 0;

    public bool HasSingle => (Bits & (Bits - 1)) == 0 && Bits != 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int First() => BitOperations.TrailingZeroCount(Bits);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Last() => BitOperations.Log2(Bits);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int value) => (Bits & (1u << value)) is not 0;

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Indexes other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Indexes other) => Bits == other.Bits;

    public override int GetHashCode() => (int)Bits;

    public override string ToString() => string.Join(',', this);

    public static bool operator ==(Indexes l, Indexes r) => l.Equals(r);

    public static bool operator !=(Indexes l, Indexes r) => !(l == r);

    public static Indexes operator |(Indexes vals, int val) => new(vals.Bits | (1U << val));

    public static Indexes operator &(Indexes l, Indexes r) => new(l.Bits & r.Bits);

    public static Indexes operator |(Indexes l, Indexes r) => new(l.Bits | r.Bits);

    public static Indexes operator ^(Indexes l, Indexes r) => new(l.Bits & ~r.Bits);

    public static Indexes operator ^(Indexes indexes, int index) => new(indexes.Bits & ~(1U << index));

    public static Indexes operator +(Indexes indexes, int up) => new(indexes.Bits << up);

    public static Indexes operator -(Indexes indexes, int dw) => new(indexes.Bits >> dw);

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

        void IEnumerator.Reset() => throw new NotSupportedException();

        readonly void IDisposable.Dispose() { /* Nothing to dispose. */ }
    }
}
