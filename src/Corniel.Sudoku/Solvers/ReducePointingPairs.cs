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
    public class ReducePointingPairs : Technique_old
    {
        private readonly SimpleList<int> intersection = new SimpleList<int>(9);

        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
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
                            var pre = events.Count;
                            Solve(state, region, intersected, value, events);

                            if(pre != events.Count)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void Solve(SudokuState state, SudokuRegion region, SudokuRegion intersected, uint value, ICollection<IEvent> events)
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
                        return;
                    }
                }
            }
            if (count > 1)
            {
                Fetch(value, intersection, intersected, state, events);
            }
        }

        private void Fetch(uint value, SimpleList<int> intersection, SudokuRegion intersected, SudokuState state, ICollection<IEvent> events)
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
                    events.Add(test);
                }
                else if (test is ReducedOption)
                {
                    result = test;
                }
            }

            if (result is ReducedOption)
            {
                events.Add(ReducedOptions.Ctor<ReduceNakedPairs>());
            }
        }
    }
}
