namespace Sudoku.Contracts;

/// <summary>Represents a set of cells in a Sudoku puzzle.</summary>
public interface SudokuCells
{
    /// <summary>Gets the cell for a specific position.</summary>
    SudokuCell this[Pos pos] { get; }
}

public static class SudokuCellsExtensions
{
    extension(SudokuCells cells)
    {
        public bool IsSolved => Pos.All.All(p => cells[p].Digits.HasSingle);

        public bool HasIncosistency => Pos.All.Any(p => cells[p].Digits.HasNone);
    }
}
