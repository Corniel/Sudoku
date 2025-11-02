using Sudoku.Generics;
using System.Globalization;
using System.IO;

namespace Sudoku.Restrictions;

public partial class Cage
{
    public static readonly ImmutableArray<DigitLookup<Digits>[]> Lookup = [..Init()];

    public static readonly ImmutableArray<double[]> Infos = [..GetInfos()];

    private static DigitLookup<Digits>[][] Init()
    {
        var lookup = new DigitLookup<Digits>[_9][];

        for (var bits = 2; bits < _9; bits++)
        {
            var tabels = new List<DigitLookup<Digits>>();

            using var stream = typeof(Cage).Assembly.GetManifestResourceStream($"Sudoku.Restrictions.Cage_{bits}.md")!;
            using var reader = new StreamReader(stream);
            var sum = 0;

            while (reader.ReadLine() is { } line)
            {
                if (line.StartsWith("## "))
                {
                    var split = line.Split(' ');

                    sum = int.Parse(split[1]);

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

        static Digits Parse(string s)
        {
            uint c = 0;
            foreach (var ch in s.Where(char.IsAsciiDigit))
                c |= 1u << (ch - '0');
            return new(c);
        }
    }

    private static double[][] GetInfos()
    {
        var lookup = new double[9][];

        for (var bits = 2; bits < _9; bits++)
        {
            var infos = new double[45];

            using var stream = typeof(Cage).Assembly.GetManifestResourceStream($"Sudoku.Restrictions.Cage_{bits}.md")!;
            using var reader = new StreamReader(stream);
            var sum = 0;

            while (reader.ReadLine() is { } line)
            {
                if (line.StartsWith("## ") && line.Split(' ') is { Length: > 2 } split)
                {
                    sum = int.Parse(split[1]);
                    var info = double.Parse(split[2], CultureInfo.InvariantCulture);
                    infos[sum] = info;
                }
            }
            lookup[bits] = infos;
        }

        return lookup;
    }
}
