namespace Corniel.Sudoku
{
    /// <summary>Reduces hidden singles.</summary>
    /// <remarks>
    /// Very frequently, there is only one candidate for a given row, column or
    /// sub square, but it is hidden among other candidates.
    /// </remarks>
    internal class ReduceHiddenSingles : ISudokuSolver
    {
        /// <summary>Solves the Sudoku by reducing hidden singles.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.None;

            foreach (var region in puzzle.Regions)
            {
                foreach (var singleValue in puzzle.SingleValues)
                {
                    var cnt = 0;
                    var found = -1;
                    foreach (var index in region)
                    {
                        var val = state[index];
                        if ((val & singleValue) != SudokuPuzzle.Invalid)
                        {
                            unchecked { cnt++; }
                            if (state.IsKnown(index))
                            {
                                found = -1;
                                break;
                            }
                            else if (cnt == 1)
                            {
                                found = index;
                            }
                        }
                    }
                    if (cnt == 1 && found != -1)
                    {
                        result |= state.AndMask(found, singleValue);
                    }
                    else if (cnt == 0)
                    {
                        return ReduceResult.Inconsistent;
                    }
                }
            }
            return result;
        }
    }
}
