namespace Corniel.Sudoku
{
    /// <summary>Reduces naked pairs.</summary>
    /// <remarks>
    /// If two cells in a group contain an identical pair of candidates and only
    /// those two candidates, then no other cells in that group could be those
    /// values.
    /// 
    /// These 2 candidates can be excluded from other cells in the group.
    /// </remarks>
    internal class ReduceNakedPairs : ISudokuSolver
    {
        /// <summary>Solves the Sudoku by reducing naked pairs.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.None;

            foreach (var singleValue in puzzle.SingleValues)
            {
                foreach (var region in puzzle.Regions)
                {
                    var index0 = -1;
                    var index1 = -1;

                    var match = singleValue;

                    foreach (var index in region)
                    {
                        var value = state[index];
                        if (!state.IsKnown(index) && (value & match) != SudokuPuzzle.Invalid)
                        {
                            match |= value;

                            if /**/ (index0 == -1) { index0 = index; }
                            else if (index1 == -1) { index1 = index; }
                            else/**/{ index1 = -1; break; }
                        }
                    }
                    // We found 2 cells.
                    if (index1 != -1 && SudokuCell.Count(match) == 2)
                    {
                        foreach (var index in region)
                        {
                            if (index != index0 && index != index1)
                            {
                                result |= state.AndMask(index, ~match);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
