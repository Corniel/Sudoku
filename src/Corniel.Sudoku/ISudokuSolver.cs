namespace Corniel.Sudoku
{
    public interface ISudokuSolver
    {
        ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state);
    }
}
