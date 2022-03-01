namespace Corniel.Sudoku
{
    /// <summary>A solver for Sudoku puzzles.</summary>
    public class Solver
    {
        /// <summary>Initializes a new solver for a given puzzle.</summary>
        public Solver(SudokuPuzzle puzzle)
        { 
            Puzzle = puzzle ?? throw new ArgumentNullException(nameof(puzzle));
            Technique = new MixedSolver();
        }

        /// <summary>Gets the puzzle (structure) used by this solver.</summary>
        public SudokuPuzzle Puzzle { get; }

        /// <summary>The <see cref="Sudoku.Technique_old"/> that is used.</summary>
        private Technique_old Technique { get; }

        /// <summary>Solves a Sudoku puzzle given the Sudoku state.</summary>
        public SudokuState Solve(SudokuState sudokuState)
        {
            // As states are mutable, create a copy.
            var state = sudokuState.Copy();
            var events = new List<IEvent>(128);
            Technique.Solve(Puzzle, state, events);
            foreach(var @event in events)
            {
                Console.WriteLine(@event);
            }
            return state;
        }
    }
}
