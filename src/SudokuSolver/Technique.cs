namespace SudokuSolver;

/// <summary>A technique to (partly) solve a Sudoku puzzle.</summary>
/// <remarks>See: http://www.taupierbw.be/SudokuCoach/SC_index.shtml</remarks>
public interface Technique
{
    /// <summary>Reduces the options per cell.</summary>
    Cells? Reduce(Cells cells, Regions regions);
}
