using Puzzles;
using Puzzles.CrackingTheCryptic;
using Puzzles.Kaggle;
using Puzzles.NewYorkTimes;
using Puzzles.PuzzleBank;
using Qowaiv.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;

namespace Sudoku.App;

public static class TestSets
{
    private static readonly DirectoryInfo Root = new(Path.Combine(typeof(TestSets).Assembly.Location, "../../../../../"));

    public static void SolveAll(
        bool dlx = true,
        bool refr = true)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        Console.Write($"{Style.H1_Pipe}|");
        Console.Write($"{Style.H1} Set                  {Style.H1_Pipe}|");
        Console.Write($"{Style.H1} Puzzles {Style.H1_Pipe}|");
        Console.Write($"{Style.H1}         Dynamic{Style.H1_Pipe}|");
        Console.Write($"{Style.H1}Solver        {Style.H1_Pipe}|");
        
        if (dlx)
            Console.Write($"{Style.H1}         Knuth’s{Style.H1_Pipe}|{Style.H1}DLX           {Style.H1_Pipe}|         |");
        if (refr)
            Console.Write($"{Style.H1}       Reference{Style.H1_Pipe}|{Style.H1}impl.         {Style.H1_Pipe}|         |");
        
        Console.WriteLine(Ansi.Reset);
        Console.Write("|:---------------------|--------:|---------------:|-------------:|");

        if (dlx)
            Console.Write("---------------:|-------------:|--------:|");
        if (refr)
            Console.Write("---------------:|-------------:|--------:|");

        Console.WriteLine();


        ImmutableArray<TestSet> sets =
        [
            Kaggle(),
            new($"Exchange {Ansi.Italic}(easy)     {Ansi.Not.Italic}", [.. PuzzleBankPuzzle.Easy.Select(p => p.Clues)]),
            new($"Exchange {Ansi.Italic}(medium)   {Ansi.Not.Italic}", [.. PuzzleBankPuzzle.Medium.Select(p => p.Clues)]),
            new($"Exchange {Ansi.Italic}(hard)     {Ansi.Not.Italic}", [.. PuzzleBankPuzzle.Hard.Select(p => p.Clues)]),
            new($"Exchange {Ansi.Italic}(diabolic) {Ansi.Not.Italic}", [.. PuzzleBankPuzzle.Diabolical.Select(p => p.Clues)]),
            new($"Exchange  {Ansi.Italic}(1000*)   {Ansi.Not.Italic}", [.. PuzzleBankPuzzle.Diabolical.OrderByDescending(p => p.Level).Select(p => p.Clues).Take(1000)]),
            new($"Generated {Ansi.Italic}(hard)    {Ansi.Not.Italic}", [..GeneratedPuzzle.Load(new FileInfo(Path.Combine(Root.FullName, "../sudoku-puzzles/generated.hard.txt"))).Select(p => p.Clues)]),
            new($"{Ansi.Italic}The New York Times  {Ansi.Not.Italic}", [.. NewYorkTimesPuzzle.Hard.Select(p => p.Clues)]),
            new("Cracking the Cryptic", [.. CtcPuzzle.Classics.Select(p => p.Clues)]),
        ];

        var sw = new Stopwatch();

        var warmup = new List<Clues>();
        foreach (var set in sets)
            warmup.AddRange(set.Clues.Take(100));
        sets = [new("Warmup              ", [.. warmup]), .. sets];

        foreach (var set in sets)
        {
            Console.Write($"| {set.Name} ");
            Console.Write($"| {Style.Gray}{set.Clues.Length,7:#,###}{Ansi.Default} ");

            var reference = Test(set, clues => DynamicSolver.Solver.Raw(clues, RuleSet.Standard));

            if (dlx)
            {
                Test(set, Dlx.DlxSolver.Solve, reference);
            }
            if (refr)
            {
                Test(set, Reference.Solver.Raw, reference);
            }
            Console.WriteLine("|");
        }
    }

    private static TimeSpan Test<T>(TestSet set, Func<Clues, T> run, TimeSpan? reference = null)
    {
        var sw = Stopwatch.StartNew();
        var best = TimeSpan.MaxValue;

        for (var i = 0; i < 10_000; i += set.Clues.Length)
        {
            sw.Restart();

            foreach (var clues in set.Clues)
                _ = run(clues);

            sw.Stop();

            if (sw.Elapsed < best)
                best = sw.Elapsed;
        }

        Log(best, set);

        if (reference is { } r)
            Log(best, r);

        return best;
    }

    private static void Log(TimeSpan sw, TestSet set)
        => Console.Write($"| {set.Clues.Length / sw.TotalMilliseconds,10:#,##0.00} {Style.Gray}k/s{Ansi.Default} | {sw.TotalMicroseconds / set.Clues.Length,9:#,##0.00} {Style.Gray}μs{Ansi.Default} ");

    private static void Log(TimeSpan sw, TimeSpan reference)
        => Console.Write($"|{Style.Yellow} {sw.TotalSeconds / reference.TotalSeconds,6:0.00} {Ansi.Default} ");

    private static TestSet Kaggle()
    {
        var clues = new List<Clues>();
        using var reader = new StreamReader(Path.Combine(Root.FullName, "../sudoku-kaggle/sudoku.csv"));
        clues.AddRange(KagglePuzzle.Load(reader).Select(p => p.Clues));
        return new($"Kaggle {Ansi.Italic}(300k)       {Ansi.Not.Italic}", [.. clues.OrderBy(c => c.Count).Take(300_000)]);
    }

    private sealed record TestSet(string Name, ImmutableArray<Clues> Clues);
}
