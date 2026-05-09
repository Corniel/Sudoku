namespace Sudoku.Contracts;

/// <summary>A rule where the linked cells have a defined sum.</summary>
public interface Summation : Rule
{
    /// <summary>The outcome of the sum.</summary>
    Ints Sum { get; }
}
