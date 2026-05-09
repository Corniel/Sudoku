namespace Sudoku.Parsing;

/// <summary>Represents a line on grid.</summary>
[DebuggerDisplay("{Name}, Length = {Length}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly record struct Line(PosArray Cells, char First, char Last) : IReadOnlyList<Pos>
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    /// <summary>Gets the name of the line.</summary>
    public string Name => Chars[Chars.IndexOf(First)..(Chars.IndexOf(Last) + 1)];

    /// <summary>Reprsents the cells as a set.</summary>
    public PosSet Set => [.. Cells];

    /// <summary>Length of the line.</summary>
    public int Length => Cells.Length;

    /// <inheritdoc />
    public Pos this[int index] => Cells[index];

    public PosArray this[Range range] => Cells[range];

      /// <inheritdoc cref="ImmutableArray{T}.IndexOf(T)" />
    public int IndexOf(Pos pos) => Cells.IndexOf(pos);

    /// <inheritdoc />
    public IEnumerator<Pos> GetEnumerator() => Cells.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    int IReadOnlyCollection<Pos>.Count => Cells.Length;

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
