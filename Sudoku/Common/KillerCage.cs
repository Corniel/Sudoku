using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed partial class KillerCage(int sum, PosSet cells) : Set([.. cells])
{
    public int Sum { get; } = sum;

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(sum, cells)];

    internal override string DebuggerDisplay => $", Sum = {Sum}";

    private static IEnumerable<Restriction> Reducers(int sum, PosSet cells) => cells.Count switch
    {
        _ when sum is 0 => [],
        1 => [new Mask(cells.First(), [sum])],
        2 => Sudoku.Restrictions.Sum.New(cells.First(), cells.Last(), sum),
        9 => [],
        _ => Group.Select(cells, (appliesTo, others) => new Cage(appliesTo, others, [sum])),
    };
}
