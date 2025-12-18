namespace Sudoku;

public static class Dominos
{
    /// <summary>Gets all domino's (pairs of cells that are connect horizontal or vertical.</summary>
    public static readonly ImmutableArray<Domino> All = [.. Init().Order()];

    public static readonly ImmutableArray<Domino> Hor = [.. All.Where(d => d.IsHor)];
    public static readonly ImmutableArray<Domino> Ver = [.. All.Where(d => d.IsVer)];

    private static IEnumerable<Domino> Init()
    {
        foreach (var p in Pos.All)
        {
            if (p.S() is { } s)
            {
                yield return new(p, s);
            }
            if (p.W() is { } w)
            {
                yield return new(p, w);
            }
        }
    }
}
