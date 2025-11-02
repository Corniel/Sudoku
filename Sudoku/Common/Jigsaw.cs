using Sudoku.Parsing;

namespace Sudoku.Common;

/// <summary>
/// Represents an irregular (jigsaw) house, as an alternative to <see cref="Houses.Box"/>es.
/// </summary>
public sealed class Jigsaw(PosSet cells) : Set([.. cells])
{
    public override string ToString() => $"Jigsaw = {string.Join(", ", Cells)}";

    public static IEnumerable<Jigsaw> Parse(string str)
        => NamedCage.Parse(str).Select(c => new Jigsaw([.. c.Cells]));
}
