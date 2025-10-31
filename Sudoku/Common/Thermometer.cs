using Sudoku.Restrictions;

namespace Sudoku.Common;

/// <summary>
/// A themometer descibes a path where every next cell has to have a higher
/// value then all previous ones.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public sealed class Thermometer(ImmutableArray<Pos> path) : Set(path)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(path)];

    public override string ToString() => $"Thermo: {string.Join(" < ", Restrictions.Select(r => r.AppliesTo).Distinct())}";

    public static Thermometer Parse(string str)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Digit).Select(c => c.Pos)]);
    }

    private static IEnumerable<Restriction> Reducers(ImmutableArray<Pos> path)
    {
        for (var f = 0; f < path.Length - 1; f++)
        {
            for (var s = f + 1; s < path.Length; s++)
            {
                yield return new Less(path[f], path[s], s - f);
                yield return new More(path[s], path[f], s - f);
            }
        }
    }
}
