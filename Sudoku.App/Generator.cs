using Generator;
using StrategyBased;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

namespace Sudoku.App;

public static class Generator
{
    public static void Generate(int size, int? seed = null)
    {
        var random = new Random(seed ?? size);
        var generator = new PuzzleGenerator(ReduceOptions.All, random);

        using var easy = new StreamWriter("c:/TEMP/sudoku.easy.txt", true);
        using var medium = new StreamWriter("c:/TEMP/sudoku.medium.txt", true);
        using var hard = new StreamWriter("c:/TEMP/sudoku.hard.txt", true);

        var count = 0;

        foreach (var candidate in generator.Take(size))
        {
            count++;

            var writer = hard;
            //candidate.Strategies.Max() switch
            //{
            //    StrategyType.HiddenSingles => easy,
            //    > StrategyType.HiddenPairs when candidate.Strategies.Length > 
            //    var max when max <= StrategyType.HiddenPairs => medium,
            //    _ => hard,
            //};

            if (candidate.Strategies.Contains(StrategyType.HiddenSingles)
                && candidate.Strategies.Count(s =>s > StrategyType.HiddenPairs) > 1)
            {

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
            }

            if ((count % 1000) == 0 || count == size)
            {
                easy.Flush();
                medium.Flush();
                hard.Flush();

                var stats = generator.Stats;

                Console.Clear();
                Console.WriteLine("# Strategies");
                foreach (var i in stats.Strategies.NonZero())
                {
                    Console.WriteLine($"{(StrategyType)i,-14} => {stats.Strategies[i],8:#,##0}");
                }
                Console.WriteLine("# Fetches");
                foreach (var i in stats.Tries.NonZero())
                {
                    Console.WriteLine($"{i} => {stats.Fetches[i],13:#,##0} out {stats.Tries[i],13:#,##0}");
                }

                Console.WriteLine("# Reductions");
                foreach (var i in stats.Reductions.NonZero())
                {
                    Console.WriteLine($"{i,2} => {stats.Reductions[i],13:#,##0}");
                }
                Console.WriteLine("# Clue Counts");
                foreach (var i in stats.ClueCounts.NonZero())
                {
                    Console.WriteLine($"{i,2} => {stats.ClueCounts[i],13:#,##0}");
                }
                Console.CursorTop = 0;
            }
        }
    }

    private static IEnumerable<int> NonZero(this int[] numbers)
    {
        var start = 0;
        while (numbers[start] is 0) start++;
        var end = numbers.Length - 1;
        while (numbers[end] is 0) end--;

        return range(start, end - start + 1);
    }

    private static readonly FrozenDictionary<StrategyType, string> Labels = Enum.GetValues<StrategyType>()
        .Select(t => KeyValuePair.Create(t, typeof(StrategyType)
            .GetField(t.ToString())!
            .GetCustomAttribute<DisplayAttribute>()?.Name ?? string.Empty))
        .Where(kvp => kvp.Value.Length > 0)
        .ToFrozenDictionary();
}
