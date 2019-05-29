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
        public IEnumerable<IEvent> Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            foreach (var region in puzzle.Regions)
            {
                IEvent result = ReduceRegion(region, state);

                if (!(result is NoReduction))
                {
                    yield return result;
                }
            }
        }

        private IEvent ReduceRegion(SudokuRegion region, SudokuState state)
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

            if (count < 2)
            {
                return NoReduction.Instance;
            }
            return Fetch(nakedPair, region, state);
        }

        private IEvent Fetch(uint nakedPair, SudokuRegion region, SudokuState state)
        {
            var mask = ~nakedPair;

            IEvent result = NoReduction.Instance;

            foreach (var index in region)
            {
                var value = state[index];
                if (value != nakedPair)
                {
                    var test = state.And<ReduceNakedPairs>(index, mask);

                    if (test is ValueFound)
                    {
                        return test;
                    }
                    else if (test is ReducedOption)
                    {
                        result = test;
                    }
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
