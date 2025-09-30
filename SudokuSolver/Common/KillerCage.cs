using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

[SuppressMessage("Clarity", "S4050", Justification = "We only need the - operator")]
public sealed partial class KillerCage(int sum, PosSet cells) : Rule, FixedSum
{
    public int Sum { get; } = sum;

    public override bool IsSet => true;

    public override PosSet Cells { get; } = cells;

    public override ImmutableArray<Restriction> Restrictions { get; } = [.. Reducers(sum, cells)];

    internal override string DebuggerDisplay => $", Sum = {Sum}";

    public static KillerCage operator -(KillerCage cage, KillerCage other)
        => other.Cells.IsSubsetOf(cage.Cells)
        ? new(cage.Sum - other.Sum, cage.Cells ^ other.Cells)
        : cage;

    private static IEnumerable<Restriction> Reducers(int sum, PosSet cells) => cells.Count switch
    {
        _ when sum is 0 => [],
        1 => [new Mask(cells.First(), [sum])],
        9 => [],
        _ => Cages(sum, cells),
    };

    private static IEnumerable<Cage> Cages(int sum, PosSet cells)
    {
        ImmutableArray<Pos> all = [.. cells];

        foreach (Pos pos in all)
        {
            yield return new Cage(sum, pos, all.Remove(pos));
        }
    }

    private sealed class Cage(int sum, Pos appliesTo, ImmutableArray<Pos> others) : Restrictions.Cage(appliesTo, others)
    {
        public int Sum { get; } = sum;

        public override double Bits => Infos[Others.Length + 1][Sum];

        public override Candidates Restrict(Cells cells) => Restrict(cells, Sum);
    }
}
