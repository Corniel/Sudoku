namespace Sudoku.Parsing;

/// <summary>Represents a named group on grid.</summary>
[DebuggerDisplay("{Name}, Size = {Size}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly record struct NamedGroup(PosSet Cells, char Name) : IReadOnlyCollection<Pos>, GridItem
{
    /// <summary>Length the size of the group.</summary>
    public int Size => Cells.Count;

    /// <inheritdoc />
    public IEnumerator<Pos> GetEnumerator() => Cells.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    int IReadOnlyCollection<Pos>.Count => Cells.Count;

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator PosSet(NamedGroup group) => group.Cells;
}
