using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    /// <summary>Reduces naked pairs.</summary>
    /// <remarks>
    /// Two cells in a row, a column or a block having only the same pair of
    /// candidates are called a Naked Pair.
    /// 
    /// All other appearances of the two candidates in the same row, column,
    /// or block can be eliminated.
    /// </remarks>
    internal class ReduceNakedPairs : ISudokuSolver
    {
        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            foreach (var region in puzzle.Regions)
            {
                var pre = events.Count;
                ReduceRegion(region, state, events);

                if (pre != events.Count)
                {
                    return;
                }
            }
        }

        private void ReduceRegion(SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            var nakedPair = 0u;
            var count = 0;

            foreach (var index in region)
            {
                var value = state[index];

                // nothing found yet.
                if (nakedPair == default)
                {
                    if (SudokuCell.Count(value) == 2)
                    {
                        nakedPair = value;
                        count++;
                    }
                }

                // Equal to the first (potential) naked pair.
                else if (value == nakedPair && count++ > 2)
                {
                    throw new InvalidPuzzleException();
                }
            }

            if (count > 1)
            {
                Fetch(nakedPair, region, state, events);
            }
        }

        private void Fetch(uint nakedPair, SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            IEvent result = NoReduction.Instance;
            var mask = ~nakedPair;

            foreach (var index in region)
            {
                var value = state[index];
                if (value != nakedPair)
                {
                    var test = state.And<ReduceNakedPairs>(index, mask);

                    if (test is ValueFound)
                    {
                        events.Add(test);
                    }
                    else if (test is ReducedOption)
                    {
                        result = test;
                    }
                }
            }

            if (result is ReducedOption)
            {
                events.Add(ReducedOptions.Ctor<ReduceNakedPairs>());
            }
        }
    }
}
