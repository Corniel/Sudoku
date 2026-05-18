namespace Sudoku.Sets;

/// <summary>Represents a set of 9 cells (house).</summary>
[DebuggerDisplay("{Name}[{Index}], Size = 9")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public abstract class House(int index, PosSet set)
    : Set
    , IReadOnlyCollection<Pos>
    , Summation
{
    /// <summary>The index of the house.</summary>
    public int Index { get; } = index;

    /// <inheritdoc />
    public PosSet Cells { get; } = set;

    /// <summary>The name of the house.</summary>
    public string Name => GetType().Name;

    /// <inheritdoc />
    public int Count => _9;

    /// <inheritdoc />
    public Ints Sum => Ints._45;

    /// <inheritdoc cref="PosSet.Contains(Pos)" />
    public bool Contains(Pos pos) => Cells.Contains(pos);

    /// <inheritdoc />
    public override string ToString() => $"{Name}[{Index}]";

    /// <inheritdoc />
    public IEnumerator<Pos> GetEnumerator() => Cells.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator PosSet(House house) => house.Cells;
}
