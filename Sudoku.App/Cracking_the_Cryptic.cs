using Puzzles;
using Puzzles.CrackingTheCryptic;
using Qowaiv.Text;
using System.Diagnostics;

namespace Sudoku.App;

public static class Cracking_the_Cryptic
{
    public static void Run(Func<Puzzle, bool>? run = null)
    {

        Console.Write($"{Style.H1_Pipe}|");
        Console.Write($"{Style.H1} Date       {Style.H1_Pipe}|");
        Console.Write($"{Style.H1} Puzzle                                                                           {Style.H1_Pipe}|");
        Console.Write($"{Style.H1}      Speed {Style.H1_Pipe}|");
        Console.WriteLine(Ansi.Reset);
        Console.WriteLine("|:----------:|----------------------------------------------------------------------------------|-----------:|");

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

        var extra = best.O() == puzzle.Duration ? null : $" [{Style.Error}{best.O()}{Ansi.Reset} != {Style.Error}{puzzle.Duration}{Ansi.Reset}]";
        
        Console.Write($"\r{Format(puzzle, best, extra)}");
        Console.WriteLine();
    }

    private static string Format(Puzzle puzzle, TimeSpan elapsed, string? txt)
        => $"| {Style.Gray}{Date(puzzle)}{Ansi.Reset} | {Link(puzzle),-118} | {elapsed.Format(),10} | {txt ?? new string(' ', 10)}";

    private static string File(Puzzle puzzle) => puzzle.GetType().Name[1..].Replace('_', '-').Replace("-1.", "_1.");
    
    private static string Date(Puzzle puzzle) => File(puzzle)[..10];
    
    private static string Year(Puzzle puzzle) => Date(puzzle)[..4];

    private static string Link(Puzzle puzzle)
        => $"[{Style.Yellow}{puzzle.Title}{Ansi.Reset}]({Style.Url}Puzzles/CrackingTheCryptic/{Year(puzzle)}/{File(puzzle)}.cs{Style.UrlEnd})";
}
