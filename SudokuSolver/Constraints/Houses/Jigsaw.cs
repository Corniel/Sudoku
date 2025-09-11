namespace SudokuSolver.Constraints;

/// <summary>
/// Represents an irregular (jigsaw) house, as an alternative to <see cref="Box"/>es.
/// </summary>
public sealed class Jigsaw(int index, PosSet cells) : House(index, cells)
{
    public static ImmutableArray<Jigsaw> Parse(string str)
    {
        var jigsaws = new Dictionary<char, PosSet>();

        var p = Pos.O;

        foreach (var ch in str.Where(IsCell))
        {
            jigsaws.TryAdd(ch, PosSet.Empty);
            jigsaws[ch] |= p++;
        }

        return [.. jigsaws.Values.Select((set, i) => new Jigsaw(i, set))];
    }

    private static bool IsCell(char ch) => ch is '.' or '?' || char.IsAsciiLetterOrDigit(ch);
}
