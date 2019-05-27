using System.Collections.Generic;

namespace Corniel.Sudoku
{
    /// <summary>A mixed solver that uses multiple Sudoku solving techniques.</summary>
    internal class MixedSolver : ISudokuSolver
    {
        private ISudokuSolver[] solvers;
        
        private ISudokuSolver[] Solvers
        {
            get
            {
                if (solvers is null)
                {
                    solvers = new ISudokuSolver[]
                    {
                        new ReduceNakedSingles(),
                        new ReduceHiddenSingles(),
                        new ReduceNakedPairs(),
                        // new ReducePointingPairs(),
                        new ReduceLockedCandidates(),
                        new ReduceNakedTriples(),
                        new ReduceNakedQuads(),
                    };
                }
                return solvers;
            }
        }

        /// <summary>Solves a Sudoku by applying multiple techniques.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.Reduced;

            while(result.HasBeenReduced())
            {
                result = ReduceResult.None;

                for(var index = 0; index < Solvers.Length; index++)
                {
                    var solver = Solvers[index];
                    result |= solver.Solve(puzzle, state);

                    // We're done.
                    if (result.IsFinal())
                    {
                        return result; 
                    }

                    // We don't want to break on index 0 when reduced.
                    if(index != 0 && result.HasBeenReduced())
                    {
                        break;
                    }
                }
            }
            return result;
        }
    }
}
