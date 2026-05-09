namespace Sudoku.Contracts;

/// <summary>Describes a restriction.</summary>
public interface Restriction : Rule
{
    /// <summary>The cell that is restricted.</summary>
    Pos AppliesTo { get; }

    /// <summary>The remaining digits based on the restriction.</summary>
    Digits Restrict(SudokuCells cells);
}
