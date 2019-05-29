using Corniel.Sudoku.Events;
using SmartAss.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>Reduces pointing pairs.</summary>
    /// <remarks>
    /// When a certain candidate appears only in two cells in a region, and
    /// those cells are both shared with the same other region, they are called
    /// a poiting pair. All other appearances of the candidates can be
    /// eliminated.
    /// </remarks>
    public class ReducePointingPairs : ISudokuSolver
    {
        private readonly SimpleList<int> intersection = new SimpleList<int>(9);

        public IEnumerable<IEvent> Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            foreach (var region in puzzle.Regions)
            {
                foreach (var intersected in region.Intersected)
                {
                    intersection.Clear();
                    intersection.AddRange(region.Intersect(intersected));

                    // We found candidate intersections
                    if (intersection.Count > 1)
                    {
                        foreach (var value in puzzle.SingleValues)
                        {
                            var result = Solve(state, region, intersected, value);
                            if (!(result is NoReduction))
                            {
                                yield return result;
                            }
                        }
                    }
                }
            }
        }

        private IEvent Solve(SudokuState state, SudokuRegion region, SudokuRegion intersected, uint value)
        {
            var count = 0;

            foreach (var index in region)
            {
                if ((state[index] & value) != 0)
                {
                    if (intersection.Contains(index))
                    {
                        count++;
                    }
                    else
                    {
                        return NoReduction.Instance;
                    }
                }
            }
            if (count > 1)
            {
                return Fetch(value, intersection, intersected, state);
            }
            return NoReduction.Instance;
        }

        private IEvent Fetch(uint value, SimpleList<int> intersection, SudokuRegion intersected, SudokuState state)
        {
            IEvent result = NoReduction.Instance;
            var mask = ~value;

            foreach (var index in intersected)
            {
                // items in the shared section should be skipped.
                if (intersection.Contains(index))
                {
                    continue;
                }
                var test = state.And<ReducePointingPairs>(index, mask);

                if (test is ValueFound)
                {
                    return test;
                }
                else if (test is ReducedOption)
                {
                    result = test;
                }
            }

            if (result is NoReduction)
            {
                return result;
            }
            return ReducedOptions.Ctor<ReduceNakedPairs>();
        }
    }
}
