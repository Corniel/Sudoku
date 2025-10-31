namespace Sudoku.Contracts;

/// <summary>Describes a restriction.</summary>
public interface Restriction
{
    /// <summary>The cell that is restricted.</summary>
    Pos AppliesTo { get; }

    /// <summary>The linked cells.</summary>
    PosSet Links { get; }

    /// <summary>The remaining digits based on the restriction.</summary>
    Digits Restrict(SudokuCells graph);
}
