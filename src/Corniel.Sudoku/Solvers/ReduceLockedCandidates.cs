﻿namespace Corniel.Sudoku
{
    /// <summary>Reduces options that should be in the intersection.</summary>
    internal class ReduceLockedCandidates : ISudokuSolver
    {
        /// <summary>Solves the Sudoku by reducing locked candidates.</summary>
        public ReduceResult Solve(SudokuPuzzle Puzzle, SudokuState state)
        {
            var result = ReduceResult.None;

            foreach (var region in Puzzle.Regions)
            {
                foreach (var other in region.Intersected)
                {
                    ulong combined = 0;
                    foreach (var index in region)
                    {
                        if (!other.Contains(index))
                        {
                            combined |= state[index];
                        }
                    }
                    // There are options that should be in the intersection.
                    if (combined != Puzzle.Unknown)
                    {
                        foreach (var index in other)
                        {
                            if (!region.Contains(index))
                            {
                                var val = state[index];
                                var nw = val & combined;
                                result |= state.AndMask(index, nw);
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}