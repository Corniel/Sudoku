namespace SudokuSolver.Common;

/// <summary>
/// Represents an irregular (jigsaw) house, as an alternative to <see cref="Box"/>es.
/// </summary>
public sealed class Jigsaw(PosSet cells) : Set(cells)
{
    public override string ToString() => $"Jigsaw = {string.Join(", ", Cells)}";

    public static ImmutableArray<Jigsaw> Parse(string str)
    {
        var jigsaws = new Dictionary<char, PosSet>();

        var p = Pos.O;

        foreach (var ch in str)
        {
            if (ch is '.' or '?')
            {
                p++;
            }
            else if (char.IsAsciiLetterOrDigit(ch))
            {
                jigsaws.TryAdd(ch, PosSet.Empty);
                jigsaws[ch] |= p++;
            }
        }

        return [.. jigsaws.Values.Select(set => new Jigsaw(set))];
    }
}
