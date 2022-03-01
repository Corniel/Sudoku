namespace SudokuSolver;

public readonly struct Location : IEquatable<Location>
{
    public static readonly Location None = new(-1);

    private readonly int index;

    private Location(int i) => index = i;

    public override bool Equals(object? obj) => obj is Location other && Equals(other);
    
    public bool Equals(Location other) => index == other.index;
    
    public override int GetHashCode() => index;

    public override string ToString() 
        => index >= 0
        ? $"[{index / Cells.Size2}, {index % Cells.Size2}]"
        : "[?]";

    public static implicit operator int(Location location) => location.index;

    public static Location Index(int i) => new(i);

    public static Location New(int row, int col) => new(row * Cells.Size2 + col);

    public static bool operator ==(Location left, Location right)=> left.Equals(right);

    public static bool operator !=(Location left, Location right) => !(left == right);
}
