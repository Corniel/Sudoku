using Puzzles.Kaggle;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Benchmark;

public static class TestSets
{
    public static void SolveAll()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        ImmutableArray<TestSet> sets =
        [
            Kaggle(),
            new("New York Times", [.. NewYorkTimesPuzzle.Hard.Select(p => p.Clues)]),
            new("Exchange (easy)", [.. PuzzleBankPuzzle.Easy.Select(p => p.Clues)]),
            new("Exchange (medium)", [.. PuzzleBankPuzzle.Medium.Select(p => p.Clues)]),
            new("Exchange (hard)", [.. PuzzleBankPuzzle.Hard.Select(p => p.Clues)]),
            new("Exchange (diabolic)", [.. PuzzleBankPuzzle.Diabolical.Select(p => p.Clues)]),
            new("Exchange (1000*)", [.. PuzzleBankPuzzle.Diabolical.OrderByDescending(p => p.Level).Select(p => p.Clues).Take(1000)]),
        ];

        var sw = new Stopwatch();

        var warmup = new List<Clues>();
        foreach (var set in sets)
            warmup.AddRange(set.Clues.Take(100));
        sets = [new("Warmup", [.. warmup]), .. sets];

        foreach (var set in sets)
        {
            Console.Write($"| {set.Name,-19} ");
            Console.Write($"| {set.Clues.Length,7:#,###} ");

            sw.Restart();
            foreach (var clues in set.Clues)
            {
                _ = DynamicSolver.Solver.Raw(clues, Rules.Standard);
            }
            sw.Stop();
            Log(sw, set);

            var reference = sw.Elapsed;

            sw.Restart();
            foreach (var clues in set.Clues)
            {
                _ = Dlx.DlxSolver.Raw(clues);
            }
            sw.Stop();
            Log(sw, set);
            Log(sw, reference);

            sw.Restart();
            foreach (var clues in set.Clues)
            {
                _ = Reference.Solver.Raw(clues);
            }
            sw.Stop();
            Log(sw, set);
            Log(sw, reference);

            Console.WriteLine(" |");
        }
    }

    private static void Log(Stopwatch sw, TestSet set) 
        => Console.Write($"| {set.Clues.Length / sw.Elapsed.TotalMilliseconds,8:#,##0.00} k/s | {sw.Elapsed.TotalMicroseconds/set.Clues.Length ,8:#,##0.00} μs");
    
    private static void Log(Stopwatch sw, TimeSpan reference)
        => Console.Write($"| {sw.Elapsed.TotalSeconds / reference.TotalSeconds,5:0.00} ");

    private static TestSet Kaggle()
    {
        var clues = new List<Clues>();
        using var reader = new StreamReader("../../../../../sudoku-kaggle/sudoku.csv");
        clues.AddRange(KagglePuzzle.Load(reader).Select(p => p.Clues));
        return new("Kaggle (300k)", [.. clues.OrderBy(c => c.Count).Take(300_000)]);
    }

    private sealed record TestSet(string Name, ImmutableArray<Clues> Clues);
}
