namespace Sudoku.Restrictions;

/// <summary>
/// A themometer descibes a path where every next cell has to have a higher
/// value then all previous ones.
/// </summary>
public static class Thermometer
{
    public static Rules New(PosArray line) => New(new Line(line, 'a', 'z'));

    public static Rules New(Line line) =>
    [
        new CellSet(line.Set, nameof(Thermometer)),
        .. Lookups(line.Cells, [.. range(line.Length)]),
    ];

    public static Rules New(Line line, PosSet[] sets) =>
    [
        .. Lookups(line.Cells, Deltas(line, sets)),
    ];

    private static int[] Deltas(Line line, PosSet[] sets)
    {
        var deltas = new int[line.Length];
        var min = 0;

        for (var i = 1; i < line.Length; i++)
        {
            PosSet pair = [line[i - 1], line[i]];
            if (sets.Any(pair.IsSubsetOf))
            {
                min++;
            }
            deltas[i] = min;
        }
        return deltas;
    }

    private static Rules Lookups(PosArray path, int[] deltas)
    {
        for (var f = 0; f < path.Length - 1; f++)
        {
            for (var s = f + 1; s < path.Length; s++)
            {
                var delta = deltas[s] - deltas[f];
                yield return new LookupPair(path[f], path[s], Less[delta]);
                yield return new LookupPair(path[s], path[f], More[delta]);
            }
        }
    }

    private static readonly ImmutableArray<LookupDigits> Less
        = [.. range(_9).Select(delta => LookupPair.Init(d => Digits.AtMost(d - delta)))];

    private static readonly ImmutableArray<LookupDigits> More
        = [.. range(_9).Select(delta => LookupPair.Init(d => Digits.AtLeast(d + delta)))];
}
