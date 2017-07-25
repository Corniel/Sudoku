using System.Collections.Generic;
using System.Linq;

namespace Corniel.Sudoku
{
    internal class BruteForceSolver : ISudokuSolver
    {
        public BruteForceSolver(ISudokuSolver parent)
        {
            Parent = parent;
        }

        public ISudokuSolver Parent { get; }

        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            var firstUknown = GetFirstUnknown(puzzle, state);
            
            if(firstUknown == -1)
            {
                return ReduceResult.None;
            }

            var values = GetPossibleValues(puzzle, state, firstUknown).ToArray();

            foreach (var value in values)
            {
                var copy = Apply(state, firstUknown, value);
                var result = Parent.Solve(puzzle, copy);
                if (result == ReduceResult.Solved)
                {
                    state.GetValuesFrom(copy);
                    return result;
                }
            }
            return ReduceResult.None;
        }

        private static int GetFirstUnknown(SudokuPuzzle puzzle, SudokuState state)
        {
            for(var index = 0; index < puzzle.MaximumIndex; index++)
            {
                if(state.IsUnknown(index))
                {
                    return index;
                }
            }
            return -1;
        }

        private static IEnumerable<ulong> GetPossibleValues(SudokuPuzzle puzzle, SudokuState state, int index)
        {
            foreach (var value in puzzle.SingleValues)
            {
                var values = state[index];

                if ((value & values) != 0)
                {
                    yield return value; 
                }
            }
        }

        private static SudokuState Apply(SudokuState state, int index, ulong value)
        {
            var copy = state.Copy();
            var result = copy.AndMask(index, value);

            //using (var writer = new StreamWriter(@"C:\TEMP\sudoku.txt", true))
            //{
            //    var num = 1 + Math.Log(value, 2);
            //    writer.WriteLine($"Index: {index}, Value: {num:0}, Unknowns: {copy.Unknowns}");
            //    writer.WriteLine(copy);
            //    writer.WriteLine();
            //}

            if (result == ReduceResult.Inconsistent)
            {
                throw new InvalidPuzzleException("This should never happen.");
            }
            return copy;
        }
    }
}
