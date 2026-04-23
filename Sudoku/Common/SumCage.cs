using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed partial class SumCage(Ints sum, PosSet cells) : Rule([.. cells])
{
    public SumCage(int sum, PosSet cells) : this([sum], cells) { }

    public Ints Sum { get; } = sum;

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(sum, cells)];

    internal override string DebuggerDisplay => $", Sum = {Sum}";

    private static IEnumerable<Restriction> Reducers(Ints sum, PosSet cells) => cells.Count switch
    {
        _ when sum.HasNone => [],
        1 => [new Mask(cells.First(), sum.Digits)],
        _ => Group.Select(cells, (appliesTo, others) => new Cage(appliesTo, others, sum)),
    };
}
