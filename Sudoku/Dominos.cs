namespace Sudoku;

public static class Dominos
{
    /// <summary>Gets all domino's.</summary>
    public static readonly ImmutableArray<Domino> All = [.. Init().Order()];

    /// <summary>Gets all domino's that are orthogonally connected.</summary>
    public static readonly ImmutableArray<Domino> Ort = [.. All.Where(d => d.IsOrt)];

    /// <summary>Gets all domino's that are digonally connected.</summary>
    public static readonly ImmutableArray<Domino> Dig = [.. All.Where(d => d.IsDig)];

    /// <summary>Gets all domino's that are horizontally connected.</summary>
    public static readonly ImmutableArray<Domino> Hor = [.. Ort.Where(d => d.IsHor)];

    /// <summary>Gets all domino's that are vertically connected.</summary>
    public static readonly ImmutableArray<Domino> Ver = [.. Ort.Where(d => d.IsVer)];

    private static IEnumerable<Domino> Init()
    {
        foreach (var p in Pos.All)
        {
            if (p.S() is { } s)
            {
                yield return new(p, s);
            }
            if (p.E() is { } e)
            {
                yield return new(p, e);
            }
            if (p.E()?.N() is { } ne)
            {
                yield return new(p, ne);
            }
            if (p.E()?.S() is { } se)
            {
                yield return new(p, se);
            }
        }
    }
}
