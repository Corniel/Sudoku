using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    internal class ReduceXWing : ISudokuSolver
    {
        public IEnumerable<IEvent> Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            for (var x0 = 0; x0 < 7; x0++)
            {
                for (var y0 = 0; y0 < 7; y0++)
                {
                    for (var x1 = x0 + 2; x1 < 9; x1++)
                    {
                        for (var y1 = y0 + 2; y1 < 9; y1++)
                        {
                            yield break;
                        }
                    }
                }
            }
        }
    }
}
