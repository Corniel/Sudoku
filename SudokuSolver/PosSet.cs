using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace SudokuSolver;

[CollectionBuilder(typeof(PosSet), nameof(New))]
[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct PosSet(Int128 bits) : IEquatable<PosSet>, IReadOnlyCollection<Pos>
{
    public static readonly PosSet Empty;

    public static readonly PosSet All = new((Int128.One << _9x9) - 1);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int128 Bits = bits;

    public int Count => (int)Int128.PopCount(Bits);

    public bool HasNone => Bits == 0;

    public bool HasAny => Bits != 0;

    public bool Contains(Pos pos) => (Bits & (Int128.One << pos)) != 0;

    /// <inheritdoc cref="IReadOnlySet{T}.IsSubsetOf(IEnumerable{T})" />
    public bool IsSubsetOf(PosSet other) => (other.Bits & Bits) == Bits;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pos First() => HasNone ? Pos.Invalid : new((int)Int128.TrailingZeroCount(Bits));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pos Last() => HasNone ? Pos.Invalid : new((int)Int128.Log2(Bits));

    [Pure]
    public PosSet AddRange(IEnumerable<Pos> positions)
    {
        var m = Bits;

        foreach (var pos in positions)
        {
            m |= Int128.One << pos;
        }
        return new(m);
    }

    [Pure]
    public override string ToString()
    {
        var cells = Cells.Empty;

        foreach (var (row, col) in this)
        {
            cells[row, col] = 1;
        }
        return cells.ToString();
    }

    [Pure]
    public override bool Equals(object? obj) => obj is PosSet other && Equals(other);

    [Pure]
    public bool Equals(PosSet other) => Bits == other.Bits;

    [Pure]
    public override int GetHashCode() => Bits.GetHashCode();

    [Pure]
    public static PosSet New(params IEnumerable<Pos> positions)
    {
        Int128 bits = 0;
        foreach (var pos in positions)
        {
            bits |= Int128.One << pos;
        }
        return new PosSet(bits);
    }

    [Pure]
    [OverloadResolutionPriority(1)]
    public static PosSet New(params ReadOnlySpan<Pos> positions)
    {
        Int128 bits = 0;
        foreach (var pos in positions)
        {
            bits |= Int128.One << pos;
        }
        return new PosSet(bits);
    }

    public static bool operator ==(PosSet l, PosSet r) => l.Equals(r);

    public static bool operator !=(PosSet l, PosSet r) => !(l == r);

    public static PosSet operator ~(PosSet set) => new(~set.Bits & All.Bits);

    public static PosSet operator |(PosSet set, Pos pos) => new(set.Bits | Int128.One << pos);

    public static PosSet operator ^(PosSet set, Pos pos) => new(set.Bits & ~(Int128.One << pos));

    public static PosSet operator |(PosSet l, PosSet r) => new(l.Bits | r.Bits);

    public static PosSet operator &(PosSet l, PosSet r) => new(l.Bits & r.Bits);

    public static PosSet operator ^(PosSet l, PosSet r) => new(l.Bits & ~r.Bits);

    public Iterator GetEnumerator() => new(Bits);

    IEnumerator<Pos> IEnumerable<Pos>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Iterator(Int128 mask) : IEnumerator<Pos>, IEnumerable<Pos>
    {
        private Int128 Mask = mask;

        public Pos Current { get; private set; } = new(-1);

        readonly object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (Mask == 0) return false;

            var trailing = (int)Int128.TrailingZeroCount(Mask) + 1;
            Mask >>= trailing;

            Current += trailing;
            return true;
        }

        public readonly IEnumerator<Pos> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;

        public void Reset() => throw new NotSupportedException();

        public readonly void Dispose() { /* Nothing to dispose. */ }
    }
}
