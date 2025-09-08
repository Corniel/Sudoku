using BenchmarkDotNet.Attributes;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;
using SudokuSolver.Solvers;

namespace Benchmarks;

public class Cracking_the_Cryptic
{
    private static readonly _2025_08_21 Puzzle = new();

    [Benchmark]
    public Cells The_Miracle_Sudoku_Of_Eleven()
        => DynamicSolver.Solve(Puzzle.Clues, Puzzle.Constraints);
}
