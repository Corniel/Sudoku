using System;
using System.Linq;

namespace Corniel.Sudoku
{
    /// <summary>A solver for Sudoku puzzles.</summary>
    public class SudokuSolver
    {
        /// <summary>Initializes a new solver for a given puzzle.</summary>
        public SudokuSolver(SudokuPuzzle puzzle) : this(puzzle, SudokuSolverMethods.All) { }

        /// <summary>Initializes a new solver for a given puzzle.</summary>
        public SudokuSolver(SudokuPuzzle puzzle, SudokuSolverMethods methods)
        {
            Puzzle = puzzle ?? throw new ArgumentNullException(nameof(puzzle));
            Solver = new MixedSolver(methods);
        }

        /// <summary>Gets the puzzle (structure) used by this solver.</summary>
        public SudokuPuzzle Puzzle { get; }

        /// <summary>The <see cref="ISudokuSolver"/> that is used.</summary>
        private ISudokuSolver Solver { get; }

        /// <summary>Solves a Sudoku puzzle given the Sudoku state.</summary>
        public SudokuState Solve(SudokuState sudokuState)
        {
            // As states are mutable, create a copy.
            var state = sudokuState.Copy();
            Solver.Solve(Puzzle, state);
            return state;
        }
    }
}
