using System.Diagnostics.Contracts;
using System.Numerics;

namespace Sudoku;

/// <summary>All possible digits for a cell.</summary>
[CollectionBuilder(typeof(Digits), nameof(New))]
public readonly struct Digits(uint bits) : IEquatable<Digits>, IReadOnlyCollection<int>
{
    /// <summary>Iterates through all possible combinations of digits (including none).</summary>
    public static AllIterator All => new();

    internal const uint Mask = 0b_111_111_111_0;

    public static readonly Digits None;

    /// <summary>Digits 1 up to 9.</summary>
    public static readonly Digits _1_to_9 = new(Mask);

    /// <summary>Digits 1, 2, 3, (low digits).</summary>
    public static readonly Digits _123 = [1, 2, 3];

    /// <summary>Digits 4, 5, 6, (mid digits).</summary>
    public static readonly Digits _456 = [4, 5, 6];

    /// <summary>Digits 7, 8, 9, (hi digits).</summary>
    public static readonly Digits _789 = [7, 8, 9];

    /// <summary>Digits 2, 4, 6, 8 (even digits).</summary>
    public static readonly Digits Even = [2, 4, 6, 8];

    /// <summary>Digits 1, 3, 5, 7, 9 (odd digits).</summary>
    public static readonly Digits Odd = [1, 3, 5, 7, 9];

    public static Digits New(int value) => new(1U << value);

    public static Digits New(params ReadOnlySpan<int> values)
    {
        var vals = 0U;

        foreach (var v in values)
            vals |= 1U << v;

        return new(vals);
    }

    public static Digits AtLeast(int value)
        => new(0b_111_111_111_1U << (value & gte0(value)));

    [Pure]
    public static Digits AtMost(int value) => new((2U << value) - 1);

    public static Digits Between(int min, int max)
    {
        var atl = 0b_111_111_111_1UL << (min & gte0(min));
        var atm = (2UL << gte0(max)) - 1;
        return new((uint)(atl & atm));
    }

    /// <summary>Values lower then zero are treated as zero.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int gte0(int v) => v & ~(v >> 31);

    /// <summary>The bits representing the available digits.</summary>
    public readonly uint Bits = bits & Mask;

    public bool HasNone => Bits is 0;

    public bool HasAny => Bits is not 0;

    public bool HasSingle => (Bits & (Bits - 1)) is 0 && Bits is not 0;

    public bool HasMultiple => (Bits & (Bits - 1)) is not 0;

    /// <inheritdoc />
    public int Count => BitOperations.PopCount(Bits);

    public override string ToString() => $"[{string.Join(',', this)}]";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int value) => (Bits & (1u << value)) is not 0;

    /// <inheritdoc cref="IReadOnlySet{T}.IsSubsetOf(IEnumerable{T})" />
    [Pure]
    public bool IsSubsetOf(Digits other) => (other.Bits & Bits) == Bits;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int First() => BitOperations.TrailingZeroCount(Bits) & 15;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Last() => BitOperations.Log2(Bits);

    public override bool Equals(object? obj) => obj is Digits other && Equals(other);

    public bool Equals(Digits other) => Bits == other.Bits;

    public override int GetHashCode() => (int)Bits >> 1;

    public static bool operator ==(Digits l, Digits r) => l.Equals(r);

    public static bool operator !=(Digits l, Digits r) => !(l == r);

    public static Digits operator ~(Digits vals) => new(~vals.Bits);

    public static Digits operator ^(Digits l, Digits r) => new(l.Bits & ~r.Bits);

    public static Digits operator ^(Digits vals, int val) => new(vals.Bits & ~(1U << val));

    public static Digits operator |(Digits vals, int val) => new(vals.Bits | (1U << val));

    public static Digits operator +(Digits vals, int val) => new(vals.Bits << val);

    public static Digits operator -(Digits vals, int val) => new(vals.Bits >> val);

    public static Digits operator &(Digits l, Digits r) => new(l.Bits & r.Bits);

    public static Digits operator |(Digits l, Digits r) => new(l.Bits | r.Bits);

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

    public struct AllIterator() : IEnumerator<Digits>, IReadOnlyCollection<Digits>
    {
        private int bits = -2;

        public readonly Digits Current => new((uint)bits);

        readonly object IEnumerator.Current => Current;

        public readonly int Count => 512;

        public bool MoveNext()
        {
            bits += 2;
            return bits <= Mask;
        }

        public readonly void Dispose() { }

        public void Reset() => throw new NotSupportedException();

        public readonly IEnumerator<Digits> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
