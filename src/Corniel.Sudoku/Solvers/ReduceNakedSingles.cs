using Corniel.Sudoku.Events;
using System.Collections.Generic;

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
    public class ReduceNakedSingles : Technique_old
    {
        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            var reduced = false;

            for (var index = 0; index <= puzzle.MaximumIndex; index++)
            {
                // For known cells only.
                if (state.IsKnown(index))
                {
                    var mask = ~state[index];

                    // for all groups the cell belongs to.
                    foreach (var group in puzzle.Lookup[index])
                    {
                        foreach (var target in group)
                        {
                            if (target == index)
                            {
                                continue;
                            }

                            var result = state.And<ReduceNakedSingles>(target, mask);

                            if (result is ReducedOption)
                            {
                                reduced = true;
                            }
                            else if (result is ValueFound)
                            {
                                events.Add(result);
                            }
                        }
                    }
                }
            }
            if (reduced)
            {
                events.Add(ReducedOptions.Ctor<ReduceNakedSingles>());
            }
        }
    }
}
