using Corniel.Sudoku.Events;
using SmartAss.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    internal class ReduceXWing : ISudokuSolver
    {
        private readonly SimpleList<int> buffer = new SimpleList<int>(4);

        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            Solve(puzzle, state, events, SudokuRegionType.Row, SudokuRegionType.Column);
            Solve(puzzle, state, events, SudokuRegionType.Column, SudokuRegionType.Row);
        }

        private void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events, SudokuRegionType type, SudokuRegionType otherType)
        {
            var pre = events.Count;

            foreach (var first in puzzle.Regions.Where(r => r.RegionType == type))
            {
                foreach (var second in puzzle.Regions.Where(r => r.RegionType == otherType))
                {
                    if (first != second)
                    {
                        foreach (var value in SudokuCell.Singles)
                        {
                            Solve(value, first, second, otherType, state, events);
                            if (events.Count != pre)
                            {
                                return;
                            }
                        }
                    }

                }
            }
        }

        public void Solve(uint value, SudokuRegion first, SudokuRegion second, SudokuRegionType otherType, SudokuState state, ICollection<IEvent> events)
        {
            buffer.Clear();

            for (var index = 0; index < 9; index++)
            {
                var indexFirst = first[index];
                var indexSecond = second[index];
                var joinFirst = state[indexFirst] & value;
                var joinSecond = state[indexSecond] & value;

                if (joinFirst == 0)
                {
                    if (joinSecond != 0)
                    {
                        return;
                    }
                }
                else
                {
                    if (joinSecond == 0 || buffer.Count > 3)
                    {
                        return;
                    }
                    buffer.Add(indexFirst);
                    buffer.Add(indexSecond);
                }
            }
            if (buffer.Count == 4)
            {
                var reducded = Fetch(
                    value,
                    first.Intersected.FirstOrDefault(r => r.RegionType == otherType),
                    state,
                    events);

                reducded |= Fetch(
                    value,
                    second.Intersected.FirstOrDefault(r => r.RegionType == otherType),
                    state,
                    events);

                if (reducded)
                {
                    events.Add(ReducedOptions.Ctor<ReduceXWing>());
                }
            }
        }

        private bool Fetch(uint value, SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            var reducded = false;
            var mask = ~value;

            foreach (var index in region)
            {
                if (buffer.Contains(index))
                {
                    continue;
                }
                var result = state.And<ReduceXWing>(index, mask);
                if (result is ValueFound)
                {
                    events.Add(result);
                }
                else if (result is ReducedOption)
                {
                    reducded = true;
                }
            }
            return reducded;
        }
    }
}
