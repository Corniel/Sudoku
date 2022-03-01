namespace SudokuSolver;

/// <summary>A technique to (partly) solve a Sudoku puzzle.</summary>
public interface Technique
{
    /// <summary>Reduces the options per cell.</summary>
    Cells? Reduce(Cells cells, Regions regions);
}
