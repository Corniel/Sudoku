using Corniel.Sudoku.Events;
using SmartAss.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>Reduces hidden pairs.</summary>
    /// <remarks>
    /// When a pair of candidates appears in only two cells in a row, column,
    /// or a block, but there aren't the only candidates in the cells, they are
    /// called a Hidden Pair.
    /// 
    /// All candidates other than the pair in the cells can be eliminated,
    /// yielding a Naked Pair.
    /// </remarks>
    internal class ReduceHiddenPairs : Technique_old
    {
        private readonly SimpleList<int> HiddenPairs = new SimpleList<int>(3);
        private readonly List<uint> Pairs = new List<uint>();

        public ReduceHiddenPairs()
        {
            for (var f = 1; f <= 8; f++)
            {
                for (var s = f + 1; s <= 9; s++)
                {
                    Pairs.Add(SudokuCell.Single(f) | SudokuCell.Single(s));
                }
            }
        }

        /// <inheritdoc />
        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            foreach (var region in puzzle.Regions)
            {
                foreach (var pair in Pairs)
                {
                    var pre = events.Count;

                    ReduceRegion(pair, region, state, events);

                    // We found some, let simpler strategies try again.
                    if (pre != events.Count)
                    {
                        return;
                    }
                }
            }
        }

        private void ReduceRegion(uint pair, SudokuRegion region, SudokuState state, ICollection<IEvent> events)
        {
            HiddenPairs.Clear();

            foreach (var index in region)
            {
                var and = state[index] & pair;

                // at least on of the two is pressent.
                if (and != 0)
                {
                    // If not both are present or we already had 2, return.
                    if (and != pair || HiddenPairs.Count > 1)
                    {
                        return;
                    }
                    HiddenPairs.Add(index);
                }
            }

            if (HiddenPairs.Count == 2)
            {
                Fetch(pair, HiddenPairs, state, events);
            }
        }

        private void Fetch(uint pair, SimpleList<int> hiddenPairs, SudokuState state, ICollection<IEvent> events)
        {
            IEvent result = NoReduction.Instance;

            foreach (var index in hiddenPairs)
            {
                var test = state.And<ReduceHiddenPairs>(index, pair);

                if (test is ReducedOption)
                {
                    result = test;
                }
            }
            if (result is ReducedOption)
            {
                events.Add(ReducedOptions.Ctor<ReduceHiddenPairs>());
            }
        }
    }
}
