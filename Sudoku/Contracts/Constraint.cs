namespace Sudoku.Contracts;

/// <summary>Describes a restriction.</summary>
public interface Constraint : Rule
{
    /// <summary>True if the constraint has been satisfied.</summary>
    bool IsSatisfied(SudokuCells cells);
}
