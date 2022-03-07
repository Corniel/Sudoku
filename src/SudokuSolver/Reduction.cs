namespace SudokuSolver;

public record Reduction(Puzzle Reduced, Type Technique)
{
    public override string ToString() => $"{Technique.Name}: {Reduced}";
}