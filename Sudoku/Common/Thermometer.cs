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
                var delta = s - f;
                yield return new LookupPair(path[f], path[s], Less[delta]);
                yield return new LookupPair(path[s], path[f], More[delta]);
            }
        }
    }

    private static readonly ImmutableArray<LookupDigits> Less =
    [
        .. range(_9).Select(delta => LookupPair.Init(d => Digits.AtMost(d - delta)))
    ];

    private static readonly ImmutableArray<LookupDigits> More =
    [
        .. range(_9).Select(delta => LookupPair.Init(d => Digits.AtLeast(d + delta)))
    ];
}
