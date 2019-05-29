using Corniel.Sudoku.Events;
using SmartAss.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>Reduces naked quads.</summary>
    internal class ReduceNakedQuads : ISudokuSolver
    {
        private readonly SimpleList<int> buffer = new SimpleList<int>(5);

        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            var pre = events.Count;

            foreach (var region in puzzle.Regions)
            {
                for (var skip = 0; skip < 6; skip++)
                {
                    ReduceRegion(skip, region, state, events);
                    if (pre != events.Count)
                    {
                        return;
                    }
                }
            }
        }

        private void ReduceRegion(int skip, SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            var quad = 0u;
            buffer.Clear();

            foreach (var index in region.Skip(skip))
            {
                var value = state[index];

                if (SudokuCell.Count(value) < 4)
                {
                    quad |= value;

                    // Joined the represent more then 3 values.
                    if (SudokuCell.Count(quad) > 4 || buffer.Count > 3)
                    {
                        return;
                    }
                    buffer.Add(index);
                }
            }

            if (buffer.Count == 4)
            {
                Fetch(quad, buffer, region, state, events);
            }

        }

        private void Fetch(uint quad, SimpleList<int> buffer, SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            var reduced = false;
            var mask = ~quad;

            foreach (var index in region)
            {
                if (buffer.Contains(index))
                {
                    continue;
                }
                var result = state.And<ReduceNakedTriples>(index, mask);
                if (result is ValueFound)
                {
                    events.Add(result);
                }
                else if (result is ReducedOption)
                {
                    reduced = true;
                }
            }

            if (reduced)
            {
                events.Add(ReducedOptions.Ctor<ReduceNakedTriples>());
            }
        }
    }
}

