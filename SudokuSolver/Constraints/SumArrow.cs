namespace SudokuSolver.Constraints;

public sealed class SumArrow(ImmutableArray<Pos> cells) : Constraint
{
    public static SumArrow Parse(string str)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Value).Select(c => c.Pos)]);
    }

    public override bool IsSet => true;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Sum(cells[0], cells[1..]),
        .. cells[1..].Select(c => new Arrow(cells[0], c, cells[1..].Remove(c))),
    ];

    public sealed class Sum(Pos sum, ImmutableArray<Pos> others) : Restriction
    {
        public Pos AppliesTo { get; } = sum;

        public ImmutableArray<Pos> Others { get; } = others;

        public Candidates Restrict(Cells cells)
        {
            var unk = 0;
            var min = 0;
            var max = 0;

            foreach (var val in Others.Select(o => cells[o]))
            {
                if (val is 0)
                {
                    min += ++unk;
                    max += _9 - unk;
                }
                else
                {
                    min += val;
                    max += val;
                }
            }

            return Candidates.Between(min, max);
        }
    }

    public sealed class Arrow(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Restriction
    {
        public Pos Sum { get; } = sum;

        public Pos AppliesTo { get; } = appliesTo;

        public ImmutableArray<Pos> Others { get; } = others;

        public int Size => Others.Length + 1;

        public int MinSum => Mins[Size];

        public Candidates Restrict(Cells cells)
        {
            var s = cells[Sum];

            if (s is not 0 && s < MinSum)
            {
                return Candidates.None;
            }

            var sums = s is 0 ? Candidates.AtLeast(MinSum) : [s];

            var allow = Candidates.None;
            var known = Candidates.None;

            foreach (var val in Others.Select(o => cells[o]))
                known |= val;

            foreach (var s_ in sums)
                allow |= KillerCage.Lookup[Size][s_][known];

            return allow;
        }

        private static readonly int[] Mins = [0, 1, 1 + 2, 1 + 2 + 3];
    }
}
