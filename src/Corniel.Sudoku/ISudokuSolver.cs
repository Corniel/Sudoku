using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    public interface ISudokuSolver
    {
        void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events);
    }
}
