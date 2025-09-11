using SudokuSolver.Generics;
using System.IO;

namespace SudokuSolver.Constraints;

public sealed partial class KillerCage
{
    public static readonly ImmutableArray<CandidateLookup<Candidates>[]> Lookup = [..Init()];

    private static CandidateLookup<Candidates>[][] Init()
    {
        var lookup = new CandidateLookup<Candidates>[_9][];

        for (var bits = 2; bits < _9; bits++)
        {
            var tabels = new List<CandidateLookup<Candidates>>();

            using var stream = typeof(KillerCage).Assembly.GetManifestResourceStream($"SudokuSolver.Constraints.KillerCage_{bits}.md")!;
            using var reader = new StreamReader(stream);
            var sum = 0;

            while (reader.ReadLine() is { } line)
            {
                if (line.StartsWith("## "))
                {
                    sum = int.Parse(line[3..]);
                    while (tabels.Count <= sum)
                    {
                        tabels.Add(null!);
                    }
                    tabels[sum] = new();
                }
                else
                {
                    var split = line.Split('=');
                    tabels[sum][Parse(split[0])] = Parse(split[1]);
                }
            }
            lookup[bits] = [.. tabels];
        }

        return lookup;

        static Candidates Parse(string s)
        {
            uint c = 0;
            foreach (var ch in s.Where(char.IsAsciiDigit))
                c |= 1u << (ch - '0');
            return new(c);
        }
    }
}
