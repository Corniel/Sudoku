namespace Corniel.Sudoku
{
    /// <summary>Reduces (naked) singles.</summary>
    /// <remarks>
    /// Any cells which have only one candidate can safely be assigned that value.
    /// 
    /// It is very important whenever a value is assigned to a cell, that this
    /// value is also excluded as a candidate from all other blank cells sharing
    /// the same row, column and sub square.
    /// </remarks>
    public class ReduceNakedSingles : ISudokuSolver
    {
        /// <summary>Solves the Sudoku by reducing singles.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.Reduced;

            while (result == ReduceResult.Reduced)
            {
                result = ReduceResult.None;

                for (var index1 = 0; index1 <= puzzle.MaximumIndex; index1++)
                {
                    if (state.IsKnown(index1))
                    {
                        foreach (var group in puzzle.Lookup[index1])
                        {
                            foreach (var index0 in group)
                            {
                                if (index0 != index1)
                                {
                                    result |= state.Exclude(index0, index1);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
