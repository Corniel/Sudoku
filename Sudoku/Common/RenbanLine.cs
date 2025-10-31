using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class RenbanLine(ImmutableArray<Pos> cells) : Set(cells)
{
    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Pairs(cells)];

    private static IEnumerable<DeltaMax> Pairs(ImmutableArray<Pos> cells)
    {
        var delta = cells.Length - 1;
        for (var f = 0; f < delta; f++)
        {
            for (var s = f + 1; s <= delta; s++)
            {
                yield return new(cells[f], cells[s], delta);
                yield return new(cells[s], cells[f], delta);
            }
        }
    }
}
