namespace SudokuSolver.Constraints;

public sealed class RenbanLine(ImmutableArray<Pos> cells) : Constraint
{
    public override bool IsSet => true;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        .. cells.Select(c => new Reduce(c, cells.Remove(c))),
    ];

    public sealed class Reduce(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
    {
        public Pos AppliesTo { get; } = appliesTo;

        public ImmutableArray<Pos> Others { get; } = others;

        public Candidates Restrict(Cells cells)
        {
            var min = int.MaxValue;
            var max = int.MinValue;

            foreach (var val in Others.Select(o => cells[o]).Where(v => v is not 0))
            {
                min = Math.Min(min, val);
                max = Math.Max(max, val);
            }

            if (min is int.MaxValue) return Candidates._1_to_9;

            var dt = Others.Length - (max - min);
            return Candidates.Between(min - dt, max + dt);
        }
    }
}
