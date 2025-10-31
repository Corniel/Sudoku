using Puzzles;
using Puzzles.CrackingTheCryptic;
using SudokuSolver;
using System;
using System.Diagnostics;
using System.Linq;

namespace Benchmarks;

public static class Cracking_the_Cryptic
{
    public static void Run()
    {
        foreach (var puzzle in CtcPuzzle.All.OrderByDescending(p => p.GetType().Name))
        {
            if (puzzle.Duration == O.oo)
            {
                Console.WriteLine(Format(puzzle, "skipped"));
            }
            else
            {
                Test(puzzle);
            }
        }
    }

    private static void Test(Puzzle puzzle)
    {
        var sw = Stopwatch.StartNew();
        var best = TimeSpan.MaxValue;

        var total = TimeSpan.Zero;

        for (var a = 0; a < 10 && total < TimeSpan.FromMinutes(3); a++)
        {
            sw.Restart();
            var solution = Solver.Solve(puzzle.Clues, puzzle.Constraints, ReduceOptions.All);
            sw.Stop();

            if (puzzle.Solution != solution)
            {
                Console.WriteLine(Format(puzzle, "can not be solved."));
                return;
            }
            Console.Write($"\r{Format(puzzle, sw.Elapsed.Format())}");

            if (sw.Elapsed < best) best = sw.Elapsed;

            total += sw.Elapsed;
        }

        Console.WriteLine($"\r{Format(puzzle, best.Format())}");
    }

    private static string Format(Puzzle puzzle, string txt)
        => $"{puzzle.GetType().Name[1..].Replace('_', '-')}: {puzzle.Title}: {txt}         ";
}
