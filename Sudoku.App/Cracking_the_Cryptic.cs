using Puzzles;
using Puzzles.CrackingTheCryptic;
using System.Diagnostics;

namespace Sudoku.App;

public static class Cracking_the_Cryptic
{
    public static void Run(Func<Puzzle, bool>? run = null)
    {
        var selected = run ?? (p => p.Duration is not O.oo);

        foreach (var puzzle in CtcPuzzle.All
            .Where(selected)
            .OrderByDescending(p => p.GetType().Name))
        {
            Test(puzzle);
        }
    }

    private static void Test(Puzzle puzzle)
    {
        var sw = Stopwatch.StartNew();
        var best = TimeSpan.MaxValue;

        var total = TimeSpan.Zero;

        Console.Write($"\r{Format(puzzle, "..")}");

        for (var a = 0; a < 1000 && total < TimeSpan.FromMinutes(3); a++)
        {
            sw.Restart();
            var solution = DynamicSolver.Solver.Solve(puzzle.Clues, puzzle.Constraints);
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

        Console.Write($"\r{Format(puzzle, best.Format())}");

        if (best.O() != puzzle.Duration)
        {
            Console.Write($" [{best.O()} != {puzzle.Duration}]");
        }
        Console.WriteLine();
    }

    private static string Format(Puzzle puzzle, string txt)
        => $"{puzzle.GetType().Name[1..].Replace('_', '-')}: {puzzle.Title}: {txt}         ";
}
