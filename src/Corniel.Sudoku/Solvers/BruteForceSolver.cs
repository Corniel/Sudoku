using System;
using System.Linq;

namespace Corniel.Sudoku
{
    internal class BruteForceSolver : ISudokuSolver
    { 
        private ISudokuSolver Solver { get; set; }

        public ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state)
        {
            if (Solver == null)
            {
                Solver = new MixedSolver(SudokuSolverMethods.All ^ SudokuSolverMethods.BruteForce);
            }
            return Solve(puzzle, state, Cursor.Initial);
        }

        private ReduceResult Solve(SudokuPuzzle puzzle, SudokuState state, Cursor cursor)
        {
            while(cursor.IsNotDone())
            {
                if(cursor.IsUnknown(puzzle, state))
                {
                    Console.WriteLine(cursor);

                    var copy = state.Copy();
                    var mask = puzzle.GetSingleValue(cursor.Value);
                    var result = copy.AndMask(cursor.Index, mask);

                    if (result.HasFlag(ReduceResult.Inconsistent))
                    {
                        return Solve(puzzle, state, cursor.Next(puzzle));
                    }

                    result = Solver.Solve(puzzle, copy);

                    // Inconsistent, try next.
                    if (result.HasFlag(ReduceResult.Inconsistent))
                    {
                        return Solve(puzzle, state, cursor.Next(puzzle));
                    }
                    // We're done, return.
                    if (result == ReduceResult.Solved)
                    {
                        state.GetValuesFrom(copy);
                        return result;
                    }
                    // Continue with the copy.
                    return Solve(puzzle, copy, cursor.Next(puzzle));
                }
                cursor = cursor.Next(puzzle);
            }
            return ReduceResult.None;
        }

        private struct Cursor
        {
            public readonly int Index;
            public readonly int Value;

            public static readonly Cursor Done = new Cursor(int.MaxValue, int.MaxValue);
            public static readonly Cursor Initial = new Cursor(0, 0);

            public Cursor(int idx, int val)
            {
                Index = idx;
                Value = val;
            }

            public Cursor Next(SudokuPuzzle puzzle)
            {
                if(Value < puzzle.SingleValues.Count)
                {
                    return new Cursor(Index, Value + 1);
                }
                if(Index < puzzle.MaximumIndex)
                {
                    return new Cursor(Index + 1, 0);
                }
                return Done;
            }

            public bool IsNotDone() => !Equals(Done);

            public bool IsUnknown(SudokuPuzzle puzzle, SudokuState state)
            {
                if(state.IsUnknown(Index))
                {
                    var values = state[Index];
                    var mask = puzzle.GetSingleValue(Value);
                    return (values & mask) != 0;
                }
                return false;
            }

            public override string ToString() => $"index: {Index}, value: {Value}";
        }
    }
}
