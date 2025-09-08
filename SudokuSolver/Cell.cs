namespace SudokuSolver;

public readonly struct Cell(Pos pos, int value) : IEquatable<Cell>
{
    public readonly Pos Pos = pos;

    public readonly int Value = value;

    public void Deconstruct(out int row, out int col, out int value)
    {
        (row, col) = Pos;
        value = Value;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Cell other && other.Equals(this);

    /// <inheritdoc />
    public bool Equals(Cell other) => Pos == other.Pos && Value == other.Value;

    /// <inheritdoc />
    public override int GetHashCode() => (int)Pos | (Value << 7);

    /// <inheritdoc />
    public override string ToString() => $"{Pos} = {(Value is 0 ? "?" : Value.ToString())}";
}
