namespace SudokuSolver;

/// <summary>Represents a (distinct) Sudoku region.</summary>
public class Region : IEnumerable<Location>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IReadOnlyCollection<Location> Locations;

    /// <summary>Creates a Sudoku region.</summary>
    public Region(int[] indexes, RegionType type)
    {
        Locations = indexes.Select(i => Location.Index(i)).ToArray();
        Type = type;
    }

    public int Count => Locations.Count;

    /// <summary>Gets the type of the region.</summary>
    public RegionType Type { get; }
    
    /// <summary>Represents the region as <see cref="string"/>.</summary>
    public override string ToString() => $"{Type}: {string.Join(",", Locations)}";

    public IEnumerator<Location> GetEnumerator()=> Locations.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
