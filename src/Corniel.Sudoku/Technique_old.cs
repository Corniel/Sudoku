using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    public interface Technique_old
    {
        void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events);
    }
}
