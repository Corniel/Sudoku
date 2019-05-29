using Corniel.Sudoku.Events;
using System.Collections.Generic;

namespace Corniel.Sudoku
{
    /// <summary>Reduces hidden singles.</summary>
    /// <remarks>
    /// A cell with multiple candidates is called a hidden single if one of the
    /// candidates is the only candidate is a row, collumn or block. The single
    /// candidate is the solution to the cell.
    /// 
    /// All other appearences of the same candidate, if any, are eliminated if
    /// they can bee seen by the Single.
    /// </remarks>
    internal class ReduceHiddenSingles : ISudokuSolver
    {
        private const int NoIndex = -1;

        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            // check all groups.
            foreach (var region in puzzle.Regions)
            {
                foreach (var singleValue in puzzle.SingleValues)
                {
                    var pre = events.Count;
                    CheckCells(state, region, singleValue, events);

                    if (pre != events.Count)
                    {
                        return;
                    }
                }
            }
        }

        private void CheckCells(SudokuState state, SudokuRegion region, uint singleValue, ICollection<IEvent> events)
        {
            var hidden = NoIndex;

            foreach (var index in region)
            {
                var value = state[index];

                // the cell has the value.
                if ((singleValue & value) != 0)
                {
                    // Already value, try next.
                    if (singleValue == value)
                    {
                        return;
                    }

                    // not the first
                    if (hidden != NoIndex)
                    {
                        return;
                    }
                    hidden = index;
                }
            }

            if (hidden == NoIndex)
            {
                return;
            }
            events.Add(state.And<ReduceHiddenSingles>(hidden, singleValue));
        }
    }
}
