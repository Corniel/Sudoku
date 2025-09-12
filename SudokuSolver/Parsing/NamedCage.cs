using System.Text.RegularExpressions;

namespace SudokuSolver.Parsing;

public sealed partial record NamedCage()
{
    public required char Name { get; init; }

    public required int Sum { get; init; }

    public ImmutableArray<Pos> Cells { get; init; } = [];

    public override string ToString()
        => Sum is 0
        ? $"{Name} = ?, {string.Join(", ", Cells)}"
        : $"{Name} = {Sum,-2}, {string.Join(", ", Cells)}";

    public static ImmutableArray<NamedCage> Parse(string str)
    {
        var take = str.Length;

        var named = new Dictionary<char, NamedCage>();
        var singles = new List<NamedCage>();

        var matches = Pattern().Matches(str);

        if (matches is { Count: > 0 })
        {
            take = matches[0].Index;

            foreach (var cage in matches.Select(FromMatch))
            {
                named[cage.Name] = cage;
            }
        }

        Pos p = default;

        foreach (var ch in str.Take(take))
        {
            if (ch is '.')
            {
                p++;
            }
            else if (char.IsAsciiLetter(ch))
            {
                named.TryAdd(ch, new() { Name = ch, Sum = 0 });
                var name = named[ch];
                named[ch] = name with { Cells = name.Cells.Add(p++) };
            }
            else if (char.IsAsciiDigit(ch))
            {
                singles.Add(new() { Name = ch, Sum = ch - '0', Cells = [p++] });
            }
        }

        return named.Count is not 0
            ? [.. named.Values, .. singles]
            : [];
    }

    private static NamedCage FromMatch(Match m) => new()
    {
        Name = m.Groups[nameof(Name)].Value[0],
        Sum = int.Parse(m.Groups[nameof(Sum)].Value),
    };

    [GeneratedRegex(@"(?<Name>[A-Za-z])\s*=\s*(?<Sum>[0-9]{1,2})", RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture)]
    private static partial Regex Pattern();
}
