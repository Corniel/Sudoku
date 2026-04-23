using System.Text.RegularExpressions;

namespace Sudoku.Common;

public static partial class SameSums
{
    public static IEnumerable<Restriction> Parse(string str)
    {
        var matches = Pattern().Matches(str);

        var end = matches.Count is 0 ? str.Length : matches.OfType<Match>().Min(m => m.Index);

        var groups = Groups.Parse(str[..end]);

        HashSet<char> unique = [.. groups.Select(g => g.Key)];

        foreach (Match match in matches)
        {
            var names = match.ToString().Where(char.IsAsciiLetter).ToArray();

            foreach (var n in names)
                unique.Remove(n);

            if (names.Any(n => !groups.ContainsKey(n)))
                throw new FormatException($"The '{string.Concat(names)}' group is not fully coverted");

            foreach (var res in SameSum.Create([..names.Select(n => groups[n].ToImmutableArray())]))
                yield return res;
        }

        if (unique.Any())
            throw new FormatException($"The '{string.Concat(unique)}' groups are not described");

        if (matches.Count is 0)
            foreach (var res in SameSum.Create([..groups.Select(g => g.Value.ToImmutableArray())]))
                yield return res;
    }

    [GeneratedRegex(@"[A-Za-z](\s*=\s*[A-Za-z])+", RegexOptions.CultureInvariant)]
    private static partial Regex Pattern();
}
