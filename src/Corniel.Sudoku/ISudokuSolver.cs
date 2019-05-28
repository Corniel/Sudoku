using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    public interface ISudokuSolver
    {
        IEnumerable<IEvent> Solve(SudokuPuzzle puzzle, SudokuState state);
    }
    public interface ISudokuSolverOld
    {
        ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state);
    }
}
