using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class RenbanLine(params ImmutableArray<Pos> cells) : Set(cells)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Pairs(cells)];

    private static IEnumerable<Pair> Pairs(ImmutableArray<Pos> cells)
    {
        // If the length is _9, there are not othere restrictions.
        if (cells.Length is _9) yield break;

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
}
