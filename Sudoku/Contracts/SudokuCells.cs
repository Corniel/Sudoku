namespace Sudoku.Contracts;

/// <summary>Represents a set of cells in a Sudoku puzzle.</summary>
public interface SudokuCells
{
    /// <summary>Gets the cell for a specific position.</summary>
    SudokuCell this[Pos pos] { get; }

    [Obsolete]
    Digits Test(Pos pos);
}
