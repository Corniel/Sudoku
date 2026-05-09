using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.Kaggle;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Sudoku.App;

public static class TestSets
{
    private static readonly DirectoryInfo Root = new(Path.Combine(typeof(TestSets).Assembly.Location, "../../../../../"));

    public static void SolveAll(
        bool dlx = true,
        bool refr = true)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        ImmutableArray<TestSet> sets =
        [
            Kaggle(),
            new("Exchange (easy)", [.. PuzzleBankPuzzle.Easy.Select(p => p.Clues)]),
            new("Exchange (medium)", [.. PuzzleBankPuzzle.Medium.Select(p => p.Clues)]),
            new("Exchange (hard)", [.. PuzzleBankPuzzle.Hard.Select(p => p.Clues)]),
            new("Exchange (diabolic)", [.. PuzzleBankPuzzle.Diabolical.Select(p => p.Clues)]),
            new("Exchange (1000*)", [.. PuzzleBankPuzzle.Diabolical.OrderByDescending(p => p.Level).Select(p => p.Clues).Take(1000)]),
            new("Generated (hard)", [..GeneratedPuzzle.Load(new FileInfo(Path.Combine(Root.FullName, "../sudoku-puzzles/generated.hard.txt"))).Select(p => p.Clues)]),
            new("New York Times", [.. NewYorkTimesPuzzle.Hard.Select(p => p.Clues)]),
            new("Cracking the Cryptic", [.. CtcPuzzle.Classics.Select(p => p.Clues)]),
        ];

        var sw = new Stopwatch();

        var warmup = new List<Clues>();
        foreach (var set in sets)
            warmup.AddRange(set.Clues.Take(100));
        sets = [new("Warmup", [.. warmup]), .. sets];

        foreach (var set in sets)
        {
            Console.Write($"| {set.Name,-20} ");
            Console.Write($"| {set.Clues.Length,7:#,###} ");

            sw.Restart();
            foreach (var clues in set.Clues)
            {
                _ = DynamicSolver.Solver.Raw(clues, RuleSet.Standard);
            }
            sw.Stop();
            Log(sw, set);
            var reference = sw.Elapsed;

            if (dlx)
            {
                sw.Restart();
                foreach (var clues in set.Clues)
                {
                    _ = Dlx.DlxSolver.Raw(clues);
                }
                sw.Stop();
                Log(sw, set);
                Log(sw, reference);
            }
            if (refr)
            {
                sw.Restart();
                foreach (var clues in set.Clues)
                {
                    _ = Reference.Solver.Raw(clues);
                }
                sw.Stop();
                Log(sw, set);
                Log(sw, reference);
            }
            Console.WriteLine(" |");
        }
    }

    private static void Log(Stopwatch sw, TestSet set)
        => Console.Write($"| {set.Clues.Length / sw.Elapsed.TotalMilliseconds,10:#,##0.00} k/s | {sw.Elapsed.TotalMicroseconds / set.Clues.Length,9:#,##0.00} μs ");

    private static void Log(Stopwatch sw, TimeSpan reference)
        => Console.Write($"| {sw.Elapsed.TotalSeconds / reference.TotalSeconds,6:0.00} ");

    private static TestSet Kaggle()
    {
        var clues = new List<Clues>();
        using var reader = new StreamReader(Path.Combine(Root.FullName, "../sudoku-kaggle/sudoku.csv"));
        clues.AddRange(KagglePuzzle.Load(reader).Select(p => p.Clues));
        return new("Kaggle (300k)", [.. clues.OrderBy(c => c.Count).Take(300_000)]);
    }

    private sealed record TestSet(string Name, ImmutableArray<Clues> Clues);
}
