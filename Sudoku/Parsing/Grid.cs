using System.Text.RegularExpressions;

namespace Sudoku.Parsing;

public static partial class Grid
{
    public static IEnumerable<NamedGroup> NamedGroups(string grid)
    {
        var groups = new Dictionary<char, PosSet>();

        Pos p = default;

        foreach (var ch in grid)
        {
            if (ch is '.')
            {
                p++;
            }
            else if (char.IsAsciiLetterOrDigit(ch))
            {
                groups.TryAdd(ch, PosSet.Empty);
                groups[ch] |= p++;
            }
        }

        return groups.Select(kvp => new NamedGroup(kvp.Value, kvp.Key));
    }

    public static ImmutableArray<GridItem> Items(string grid)
    {
        var groups = new Dictionary<char, PosSet>();

        Pos p = default;
        var i = 0;

        while (p < _9x9)
        {
            var ch = grid[i++];

            if (ch is '.')
            {
                p++;
            }
            else if (char.IsAsciiLetterOrDigit(ch))
            {
                groups.TryAdd(ch, PosSet.Empty);
                groups[ch] |= p++;
            }
        }

        var items = new List<GridItem>();

        foreach (Match match in Expression().Matches(grid[i..]))
        {
            string[] operators = [.. match.Groups[nameof(GridExpression.Operator)].Captures.Select(c => c.Value)];
            string[] args = [.. match.Groups["Token"].Captures.Select(c => c.Value), match.Groups["Last"].Value];

            if (operators.Distinct().Count() is not 1)
                throw new FormatException($"Could not parse expression '{match.Value}'.");

            items.Add(new GridExpression(operators[0], [..args]));
        }

        return
        [
            .. groups.Where(kvp => char.IsAsciiLetter(kvp.Key)).Select(kvp => new NamedGroup(kvp.Value, kvp.Key)),
            .. groups.Where(kvp => char.IsAsciiDigit(kvp.Key)).SelectMany(kvp => kvp.Value.Select(x => new GridClue(x, kvp.Key - '0'))),
            .. items,
        ];
    }

    [GeneratedRegex(@"((?<Token>[A-Z]+)\s*(?<Operator>[<>=≤≥:])\s*)+(?<Last>([A-Z]+)|([1-9][0-9]*))", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
    private static partial Regex Expression();
}
