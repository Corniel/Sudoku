using System.Collections.Generic;

namespace Corniel.Sudoku
{
    /// <summary>A mixed solver that uses multiple Sudoku solving techniques.</summary>
    internal class MixedSolver : ISudokuSolver
    {
        /// <summary>Initializes a mixed solver.</summary>
        public MixedSolver(SudokuSolverMethods methods)
        {
            Methods = methods;
            Solvers = new List<ISudokuSolver>();
            AddSolver(SudokuSolverMethods.Singles, new ReduceSingles());
            AddSolver(SudokuSolverMethods.HiddenSingles, new ReduceHiddenSingles());
            AddSolver(SudokuSolverMethods.LockedCandidates, new ReduceLockedCandidates());
            AddSolver(SudokuSolverMethods.NakedPairs, new ReduceNakedPairs());
            AddSolver(SudokuSolverMethods.NakedTriples, new ReduceNakedTriples());
            AddSolver(SudokuSolverMethods.NakedQuads, new ReduceNakedQuads());
            AddSolver(SudokuSolverMethods.BruteForce, new BruteForceSolver());
        }

        /// <summary>Gets the methods that are used to try to solve the puzzle.</summary>
        public SudokuSolverMethods Methods { get; }
        private List<ISudokuSolver> Solvers { get; }

        /// <summary>Solves a Sudoku by applying multiple techniques.</summary>
        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var result = ReduceResult.Reduced;

            while(result == ReduceResult.Reduced)
            {
                result = ReduceResult.None;

                for(var index = 0; index < Solvers.Count; index++)
                {
                    var solver = Solvers[index];
                    result |= solver.Solve(puzzle, state);

                    // We don't want to break on index 0 when reduced.
                    if((index != 0 && result == ReduceResult.Reduced) || result.HasFlag(ReduceResult.Final))
                    {
                        break;
                    }
                }
            }
            return result;
        }

        private void AddSolver(SudokuSolverMethods method, ISudokuSolver solver)
        {
            if((Methods & method) != SudokuSolverMethods.None)
            {
                Solvers.Add(solver);
            }
        }
    }
}
