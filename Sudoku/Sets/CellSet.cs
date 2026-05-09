namespace Sudoku.Sets;

/// <summary>Represents a set of cells.</summary>
[DebuggerDisplay("{Name}, Size = {Size}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly record struct CellSet(PosSet Cells, string Name = "?") : Set, IReadOnlyCollection<Pos>
{
    /// <summary>Length the size of the set.</summary>
    public int Size => Cells.Count;

    /// <inheritdoc />
    public IEnumerator<Pos> GetEnumerator() => Cells.AsEnumerable().GetEnumerator();

    /// <inheritdoc />
    int IReadOnlyCollection<Pos>.Count => Cells.Count;

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
