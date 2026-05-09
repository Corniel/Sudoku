namespace Sudoku.Constraints;

/// <summary>A group of cell with a known sum.</summary>
public readonly record struct SumGroup(PosSet Cells, Ints Sum) : Summation
{
    /// <summary>Size of the sum group.</summary>
    public int Size => Cells.Count;

    /// <inheritdoc />
    public override string ToString() => $"Sum = [{Sum}], Size = {Size}, Cells = {string.Join(", ", Cells)}";
}
