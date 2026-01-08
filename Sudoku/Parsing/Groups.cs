namespace Sudoku.Parsing;

public static class Groups
{
    public static IReadOnlyDictionary<char, PosSet> Parse(string str)
    {
        var groups = new Dictionary<char, PosSet>();

        Pos p = default;

        foreach (var ch in str)
        {
            if (ch is '.')
            {
                p++;
            }
            else if (char.IsAsciiLetter(ch))
            {
                groups.TryAdd(ch, PosSet.Empty);
                groups[ch] |= p++;
            }
        }

        return groups;
    }
}
