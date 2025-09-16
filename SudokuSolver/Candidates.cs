using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SudokuSolver;

/// <summary>All possible candidate digits for a cell.</summary>
[CollectionBuilder(typeof(Candidates), nameof(New))]
public readonly struct Candidates(uint bits) : IEquatable<Candidates>, IReadOnlyCollection<int>
{
    /// <summary>Iterates through all possible combinations of candidates (including none).</summary>
    public static AllIterator All => new();

    internal const uint Mask = 0b_111_111_111_0;

    public static readonly Candidates None;

    public static readonly Candidates _1_to_9 = new(Mask);

    public static readonly Candidates Even = [2, 4, 6, 8];
    public static readonly Candidates Odd = [1, 3, 5, 7, 9];

    public static Candidates New(int value) => new(1U << value);

    public static Candidates New(params ReadOnlySpan<int> values)
    {
        var vals = 0U;

        foreach (var v in values)
            vals |= 1U << v;

        return new(vals);
    }

    public static Candidates AtLeast(int value)
        => new(0b_111_111_111_1U << (value & gte0(value)));

    [Pure]
    public static Candidates AtMost(int value) => new((2U << value) - 1);

    public static Candidates Between(int min, int max)
    {
        var atl = 0b_111_111_111_1U << (min & gte0(min));
        var atm = (2U << gte0(max)) - 1;
        return new(atl & atm);
    }

    /// <summary>Values lower then zero are treated as zero.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int gte0(int v) => v & ~(v >> 31);

    /// <summary>The bits representing the available candidates.</summary>
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int First() => BitOperations.TrailingZeroCount(Bits) & 15;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Last() => BitOperations.Log2(Bits);

    public override bool Equals(object? obj) => obj is Candidates other && Equals(other);

    public bool Equals(Candidates other) => Bits == other.Bits;

    public override int GetHashCode() => (int)Bits >> 1;

    public static bool operator ==(Candidates l, Candidates r) => l.Equals(r);

    public static bool operator !=(Candidates l, Candidates r) => !(l == r);

    public static Candidates operator ~(Candidates vals) => new(~vals.Bits);

    public static Candidates operator ^(Candidates l, Candidates r) => new(l.Bits & ~r.Bits);

    public static Candidates operator ^(Candidates vals, int val) => new(vals.Bits & ~(1U << val));

    public static Candidates operator |(Candidates vals, int val) => new(vals.Bits | (1U << val));

    public static Candidates operator +(Candidates vals, int val) => new(vals.Bits << val);

    public static Candidates operator -(Candidates vals, int val) => new(vals.Bits >> val);

    public static Candidates operator &(Candidates l, Candidates r) => new(l.Bits & r.Bits);

    public static Candidates operator |(Candidates l, Candidates r) => new(l.Bits | r.Bits);

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

    public struct AllIterator() : IEnumerator<Candidates>, IReadOnlyCollection<Candidates>
    {
        private int bits = -2;

        public readonly Candidates Current => new((uint)bits);

        readonly object IEnumerator.Current => Current;

        public readonly int Count => 512;

        public bool MoveNext()
        {
            bits += 2;
            return bits <= Mask;
        }

        public readonly void Dispose() { }

        public void Reset() => throw new NotSupportedException();

        public readonly IEnumerator<Candidates> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
