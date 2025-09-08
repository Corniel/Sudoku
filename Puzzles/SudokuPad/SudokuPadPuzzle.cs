namespace Puzzles.SudokuPad;

public abstract class SudokuPadPuzzle : Puzzle
{
    public override string? Author => "https://sudokupad.app/7gJb9G8fRt";

    public static ImmutableArray<Puzzle> All => Collect(p => p is SudokuPadPuzzle);
}
