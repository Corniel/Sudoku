namespace SudokuSolver;

[DebuggerDisplay("{ToString()} ({Index})")]
public readonly struct Pos(int i) : IEquatable<Pos>
{
    public static AllIterator All => new();

    /// <summary>The O(rigin) postion (0, 0).</summary>
    public static readonly Pos O;

    public static readonly Pos Invalid = (_9, _9);

    public Pos(int row, int col) : this((row * _9) + col) { }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Index = i;

    public Pos? N()
    {
        var r = Index / _9;
        return r is 0 ? null : new(Index - _9);
    }

    public Pos? E()
    {
        var c = Index % _9;
        return c is 8 ? null : new(Index + 1);
    }

    public Pos? S()
    {
        var r = Index / _9;
        return r is 8 ? null : new(Index + _9);
    }

    public Pos? W()
    {
        var c = Index % _9;
        return c is 0 ? null : new(Index - 1);
    }

    /// <summary>Deconstructs the position in a row and column component.</summary>
    public void Deconstruct(out int row, out int col) => (row, col) = Math.DivRem(Index, _9);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Pos other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Pos other) => Index == other.Index;

    /// <inheritdoc />
    public override int GetHashCode() => Index;

    /// <inheritdoc />
    public override string ToString()
    {
        if (Index is >= 0 and <= _9x9)
        {
            var (row, col) = this;
            return $"({row}, {col})";
        }
        else return "?";
    }

    public static implicit operator int(Pos pos) => pos.Index;

    public static Pos operator ++(Pos pos) => new(pos.Index + 1);

    public static Pos operator +(Pos pos, int steps) => new(pos.Index + steps);
    public static Pos operator -(Pos pos, int steps) => new(pos.Index - steps);

    public static implicit operator Pos((int Row, int Col) tuple) => new(tuple.Row, tuple.Col);

    public struct AllIterator() : IEnumerator<Pos>, IReadOnlyCollection<Pos>
    {
        public Pos Current { get; private set; } = new(-1);

        readonly object IEnumerator.Current => Current;

        public readonly int Count => _9x9;

        public bool MoveNext() => ++Current < _9x9;

        public readonly void Dispose() { }

        public void Reset() => throw new NotSupportedException();

        public readonly IEnumerator<Pos> GetEnumerator() => this;

        readonly IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
