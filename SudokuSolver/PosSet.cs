using System.Diagnostics.Contracts;

namespace SudokuSolver;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct PosSet(Int128 mask) : IReadOnlyCollection<Pos>
{
    public static readonly PosSet Empty;

    public static readonly PosSet All = new((Int128.One << _9x9) - 1);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Int128 Mask = mask;

    public int Count => (int)Int128.PopCount(Mask);

    public bool Contains(Pos pos) => (Mask & (Int128.One << pos)) != 0;

    [Pure]
    public PosSet AddRange(IEnumerable<Pos> positions)
    {
        var m = Mask;

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
    public static PosSet New(params IEnumerable<Pos> positions) => Empty.AddRange(positions);

    public static PosSet operator ~(PosSet set) => new(~set.Mask & All.Mask);

    public static PosSet operator |(PosSet set, Pos pos) => new(set.Mask | Int128.One << pos);

    public static PosSet operator ^(PosSet set, Pos pos) => new(set.Mask & ~(Int128.One << pos));

    public static PosSet operator |(PosSet l, PosSet r) => new(l.Mask | r.Mask);

    public static PosSet operator &(PosSet l, PosSet r) => new(l.Mask & r.Mask);

    public static PosSet operator ^(PosSet l, PosSet r) => new(l.Mask & ~r.Mask);

    public Iterator GetEnumerator() => new(Mask);

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
