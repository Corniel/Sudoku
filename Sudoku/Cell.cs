namespace Sudoku;

public readonly struct Cell(Pos pos, int digit) : IEquatable<Cell>
{
    public readonly Pos Pos = pos;

    public readonly int Digit = digit;

    public void Deconstruct(out int row, out int col, out int digit)
    {
        (row, col) = Pos;
        digit = Digit;
    }

    public void Deconstruct(out Pos pos, out int digit)
    {
        pos = Pos;
        digit = Digit;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Cell other && other.Equals(this);

    /// <inheritdoc />
    public bool Equals(Cell other) => Pos == other.Pos && Digit == other.Digit;

    /// <inheritdoc />
    public override int GetHashCode() => (int)Pos | (Digit << 7);

    /// <inheritdoc />
    public override string ToString() => $"{Pos} = {(Digit is 0 ? "?" : Digit.ToString())}";
}
