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

        Console.Write($"\r{Format(puzzle, TimeSpan.Zero, "..")}");

        for (var a = 1; a <= 1000 && total < TimeSpan.FromMinutes(3); a++)
        {
            sw.Restart();
            var solution = DynamicSolver.Solver.Solve(puzzle.Clues, puzzle.Constraints);
            sw.Stop();

            if (puzzle.Solution != solution)
            {
                Console.WriteLine(Format(puzzle, sw.Elapsed, "can not be solved."));
                return;
            }
            Console.Write($"\r{Format(puzzle, sw.Elapsed, $"{a} ..")}");

            if (sw.Elapsed < best) best = sw.Elapsed;

            total += sw.Elapsed;
        }

        var extra = best.O() == puzzle.Duration ? null : $" [{best.O()} != {puzzle.Duration}]";
        Console.Write($"\r{Format(puzzle, best, extra)}");

        Console.WriteLine();
    }

    private static string Format(Puzzle puzzle, TimeSpan elapsed, string? txt)
        => $"| {Date(puzzle)} | {Link(puzzle),-80} | {elapsed.Format(),10} | {txt ?? new string(' ', 10)}";

    private static string File(Puzzle puzzle) => puzzle.GetType().Name[1..].Replace('_', '-').Replace("-1.", "_1.");
    
    private static string Date(Puzzle puzzle) => File(puzzle)[..10];
    
    private static string Year(Puzzle puzzle) => Date(puzzle)[..4];

    private static string Link(Puzzle puzzle)
        => $"[{puzzle.Title}](Puzzles/CrackingTheCryptic/{Year(puzzle)}/{File(puzzle)}.cs)";
}
