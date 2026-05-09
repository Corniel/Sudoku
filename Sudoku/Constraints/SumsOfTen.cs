namespace Sudoku.Constraints;

/// <summary>The line can be complete chunked into pieces adding up to 10.</summary>
public sealed class SumsOfTen(PosArray line) : Constraint, Summation
{
    public PosArray Line { get; } = line[..^1];

    public Pos Last { get; } = line[^1];

    /// <inheritdoc />
    public PosSet Cells { get; } = [.. line];

    /// <inheritdoc />
    public Ints Sum => Ints.Mutlple10;

    /// <inheritdoc />
    public bool IsSatisfied(SudokuCells cells)
    {
        var total = Ints.Zero;

        foreach (var pos in Line)
        {
            total += cells[pos].Digits;

            // Filter out overflow 10.
            total &= M1_10;

            // Only overflow.
            if (total.HasNone) return false;

            total %= 10;
        }

        total += cells[Last].Digits;

        // Adds up to 10.
        return total.Contains(10);
    }

    public override string ToString() => $"Sums-of-10: Length = {Line.Length}, {string.Join(", ", Line)}";

    private static readonly Ints M1_10 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
}
