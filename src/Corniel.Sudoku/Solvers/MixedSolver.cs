using System.Collections.Generic;
using Corniel.Sudoku.Events;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>A mixed solver that uses multiple Sudoku solving techniques.</summary>
    internal class MixedSolver : ISudokuSolver
    {
        private readonly ISudokuSolver ReduceNakedSingles = new ReduceNakedSingles();
        private ISudokuSolver[] solvers;
        
        private ISudokuSolver[] Solvers
        {
            get
            {
                if (solvers is null)
                {
                    solvers = new ISudokuSolver[]
                    {
                        new ReduceHiddenSingles(),
                        new ReduceNakedPairs(),
                    };
                }
                return solvers;
            }
        }

        public IEnumerable<IEvent> Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var events = new List<IEvent>();
            var count = 0;

            do
            {
                count = events.Count;
                events.AddRange(ReduceNakedSingles.Solve(puzzle, state));
                events.AddRange(Solvers.SelectMany(solver => solver.Solve(puzzle, state).Take(1)));
            }
            while (events.Count > count);

            if (state.IsSolved)
            {
                events.Add(SolvedPuzzle.Instance);
            }
            return events;
        }
    }
}
