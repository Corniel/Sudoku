namespace Corniel.Sudoku
{
    /// <summary>Reduces naked triples.</summary>
    internal class ReduceNakedTriples: ISudokuSolver
    {
        /// <summary>Solves the Sudoku by reducing naked triples.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.None;

            foreach (var singleValue in puzzle.SingleValues)
            {
                foreach (var region in puzzle.Regions)
                {
                    var index0 = -1;
                    var index1 = -1;
                    var index2 = -1;

                    var match = singleValue;

                    foreach (var index in region)
                    {
                        var value = state[index];
                        if (!state.IsKnown(index) && (value & match) != SudokuPuzzle.Invalid)
                        {
                            match |= value;

                            if /**/ (index0 == -1) { index0 = index; }
                            else if (index1 == -1) { index1 = index; }
                            else if (index2 == -1) { index2 = index; }
                            else/**/{ index2 = -1; break; }
                        }
                    }
                    // We found 3 cells.
                    if (index2 != -1 && SudokuCell.Count(match) == 3)
                    {
                        foreach (var index in region)
                        {
                            if (index != index0 && index != index1 && index != index2)
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
