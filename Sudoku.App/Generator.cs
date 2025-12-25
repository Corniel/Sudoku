using Generator;
using Puzzles;
using StrategyBased;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace Sudoku.App;

public static class Generator
{
    private static readonly DirectoryInfo Root = new(Path.Combine(typeof(TestSets).Assembly.Location, "../../../../../"));

    public static void ReApply()
    {
        var generator = new PuzzleGenerator(ReduceOptions.All, new());
        using var writer = new StreamWriter("c:/TEMP/sudoku.generated.txt", true);

        var candidates = GeneratedPuzzle.Load(new FileInfo(Path.Combine(Root.FullName, "../sudoku-puzzles/generated.hard.txt")))
            .Select(puzzle => generator.ReApply(puzzle.Solution, puzzle.Clues));

        Generate(candidates, writer);
    }

    public static void Generate(int size, int? seed = null)
    {
        var random = new Random(seed ?? size);
        var generator = new PuzzleGenerator(ReduceOptions.All, random);
        using var writer = new StreamWriter("c:/TEMP/sudoku.generated.txt", true);

        Generate(generator, writer, size);
    }

    private static void Generate(IEnumerable<Generated> candidates, StreamWriter writer, int size = int.MaxValue)
    {
        var attempts = 0;
        var count = 0;

        var clues = new int[_9x9];
        var strategies = Enum.GetValues<StrategyType>().ToDictionary(t => t, _ => 0);

        foreach (var candidate in candidates)
        {
            attempts++;
            if (!candidate.IsChallenging()) continue;

            count++;

            candidate.Clues.WriteTo(writer);
            writer.Write(' ');
            candidate.Solution.WriteTo(writer);

            foreach (var strat in candidate.Strategies)
            {
                if (Labels.TryGetValue(strat, out var label))
                {
                    writer.Write(' ');
                    writer.Write(label);
                }
            }
            writer.Write('\n');
            writer.Flush();

            foreach (var strat in candidate.Strategies.Distinct())
                strategies[strat]++;

            clues[candidate.Clues.Count]++;

            Console.Clear();
            Console.WriteLine($"Generated: {count:#,##0} ({100m * count / attempts:0.00}% out of {attempts:#,##0})");
            Console.WriteLine();

            foreach (var (strat, cnt) in strategies
                .Where(kvp => kvp.Key is not StrategyType.None)
                .OrderByDescending(kvp => kvp.Value))
                Console.WriteLine($"{strat,-14} {cnt,8:#,##0} {100m * cnt / count,6:0.00}%");

            Console.WriteLine();

            var min = clues.Index().First(x => x.Item > 0).Index;
            var max = clues.Index().Last(x => x.Item > 0).Index;

            for (var i = min; i <= max; i++)
                Console.WriteLine($"{i}: {clues[i],8:#,##0}");

            Console.CursorTop = 0;

            if (size == count) return;
        }
    }

    /// <summary>
    /// Requires:
    /// * Hidden Singles.
    /// * At least two strategies more advanced then hidden pairs.
    /// </summary>
    private static bool IsChallenging(this Generated candidate)
        => candidate.Strategies.Count(s => s  is > StrategyType.NakedSingles and < Level) > 1
        && candidate.Strategies.Count(s => s >= Level) > 1;

    private const StrategyType Level = StrategyType.XWing;

    private static readonly FrozenDictionary<StrategyType, string> Labels = Enum.GetValues<StrategyType>()
        .Select(t => KeyValuePair.Create(t, typeof(StrategyType)
            .GetField(t.ToString())!
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? string.Empty))
        .Where(kvp => kvp.Value.Length > 0)
        .ToFrozenDictionary();
}
