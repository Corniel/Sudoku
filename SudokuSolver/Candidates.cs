using System.Diagnostics.Contracts;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace SudokuSolver;

/// <summary>All possible candidate digits for a cell.</summary>
[CollectionBuilder(typeof(Candidates), nameof(New))]
public readonly struct Candidates(uint mask) : IEquatable<Candidates>, IReadOnlyCollection<int>
{
    private static readonly uint all = 0b_111_111_111_0;

    public static Candidates New(int value) => new((1U << value) & all);

    public static Candidates New(params ReadOnlySpan<int> values)
    {
        var vals = None;

        foreach (var v in values)
        {
            vals |= v;
        }
        return vals;
    }

    public static Candidates AtLeast(int value)
        => new((0b_111_111_111_1U << (value & gte0(value))) & all);

    [Pure]
    public static Candidates AtMost(int value) => new(((2U << value) - 1) & all);

    public static Candidates Between(int min, int max)
    {
        var atl = 0b_111_111_111_1U << (min & gte0(min));
        var atm = (2U << gte0(max)) - 1;
        return new(atl & atm & all);
    }

    /// <summary>Values lower then zero are treated as zero.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int gte0(int v) => v & ~(v >> 31);

    public static readonly Candidates None;

    public static readonly Candidates All = new(all);

    public readonly uint Mask = mask;

    public bool HasNone => Mask is 0;

    public bool HasAny => Mask is not 0;

    public bool HasSingle => (Mask & (Mask - 1)) is 0 && Mask is not 0;

    public bool HasMultiple => (Mask & (Mask - 1)) is not 0;

    /// <inheritdoc />
    public int Count => BitOperations.PopCount(Mask);

    public override string ToString() => $"[{string.Join(',', this)}]";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int First() => BitOperations.TrailingZeroCount(Mask) & 15;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Last() => BitOperations.Log2(Mask);

    public override bool Equals(object? obj) => obj is Candidates other && Equals(other);

    public bool Equals(Candidates other) => Mask == other.Mask;

    public override int GetHashCode() => (int)Mask >> 1;

    public static bool operator ==(Candidates l, Candidates r) => l.Equals(r);

    public static bool operator !=(Candidates l, Candidates r) => !(l == r);

    public static Candidates operator ~(Candidates vals) => new(~vals.Mask & all);

    public static Candidates operator ^(Candidates l, Candidates r) => new(l.Mask & ~r.Mask);

    public static Candidates operator ^(Candidates vals, int val) => new(vals.Mask & ~(1U << val));

    public static Candidates operator |(Candidates vals, int val) => new((vals.Mask | (1U << val)) & all);

    public static Candidates operator +(Candidates vals, int val) => new((vals.Mask << val) & all);

    public static Candidates operator -(Candidates vals, int val) => new((vals.Mask >> val) & all);

    public static Candidates operator &(Candidates l, Candidates r) => new(l.Mask & r.Mask);

    public static Candidates operator |(Candidates l, Candidates r) => new(l.Mask | r.Mask);

    public Iterator GetEnumerator() => new(Mask);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IEnumerator<int> IEnumerable<int>.GetEnumerator() => GetEnumerator();

    public struct Iterator(uint mask) : IEnumerator<int>, IEnumerable<int>
    {
        private uint Mask = mask;

        public int Current { get; private set; } = -1;

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Mask is 0) return false;

            var trailing = BitOperations.TrailingZeroCount(Mask) + 1;
            Mask >>= trailing;

            Current += trailing;
            return true;
        }

        public readonly IEnumerator<int> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();

        public readonly void Dispose() { /* Nothing to dispose. */ }
    }
}
