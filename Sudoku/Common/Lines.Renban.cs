namespace Sudoku.Common;

public static partial class Lines
{
    /// <summary>
    /// Renban lines contain a non-repeating set of consecutive digits in any order.
    /// </summary>
    [Pure]
    public static Rules Renban(PosSet line)
    {
        if (line.Count > _9)
            throw new InvalidConstraint($"Renban line can not be longer than 9, but {line} has size {line.Count}.");

        yield return new CellSet(line, nameof(Renban));

        if (line.Count is _9) yield break;

        Pos[] cells = [.. line];

        var delta = cells.Length - 1;
        for (var f = 0; f < delta; f++)
        {
            for (var s = f + 1; s <= delta; s++)
            {
                var deltas = DeltaMax.New(cells[f], cells[s], delta);
                yield return deltas.One;
                yield return deltas.Two;
            }
        }
    }

    /// <summary>
    /// Renban lines contain a non-repeating set of consecutive digits in any order.
    /// </summary>
    [Pure]
    public static Rules Renban(string grid)
        => Grid.NamedGroups(grid).SelectMany(group => Renban(group));
}
