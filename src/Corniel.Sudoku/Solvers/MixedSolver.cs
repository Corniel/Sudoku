using System.Collections.Generic;
using Corniel.Sudoku.Events;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>A mixed solver that uses multiple Sudoku solving techniques.</summary>
    internal class MixedSolver : ISudokuSolver
    {
        private readonly ISudokuSolver[] solvers = new ISudokuSolver[]
        {
            /* 1 */ new ReduceNakedSingles(),
            /* 2 */ new ReduceHiddenSingles(),
            /* 3 */ new ReduceNakedPairs(),
            /* 4 */ new ReducePointingPairs(),
            /* 5 */ // Claimed pair handled by pointing pairs.
            /* 6 */ new ReduceNakedTriples(),
            /* 7 */ new ReduceXWing(),
            /* 8 */ new ReduceHiddenPairs(),
            /* 9 */ new ReduceNakedQuads(),
        };

        public void Solve(SudokuPuzzle puzzle, SudokuState state, ICollection<IEvent> events)
        {
            var count = -1;

            while (count != events.Count)
            {
                count = events.Count;

                for (var i = 0; i < solvers.Length; i++)
                {
                    var solver = solvers[i];
                    solver.Solve(puzzle, state, events);

                    if (events.Count != count)
                    {
                        if (state.IsSolved)
                        {
                            events.Add(SolvedPuzzle.Instance);
                            return;
                        }
                        i = solvers.Length;
                    }
                }
            }
        }
    }
}
