namespace Sudoku.Contracts;

/// <summary>Represent a specific cell in a Sudoku puzzle.</summary>
public interface SudokuCell
{
    /// <summary>Gets the position of the cell.</summary>
    Pos Pos { get; }

    /// <summary>Gets the possible digits of the cell.</summary>
    Digits Digits { get; }

    /// <summary>Gets the digit (0 if unknown) of the cell.</summary>
    int Digit { get; }
}
