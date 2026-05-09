namespace Sudoku.Restrictions;

/// <summary>
/// A themometer descibes a path where every next cell has to have a higher
/// value then all previous ones.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public static class Thermometer
{
    public static Rules New(PosArray line) => New(new Line(line, 'a', 'z'));

    public static Rules New(Line line) =>
    [
        new CellSet(line.Set, nameof(Thermometer)),
        .. Lookups(line.Cells),
    ];

    private static Rules Lookups(PosArray path)
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
