using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class Arrow(ImmutableArray<Pos> cells) : Rule
{
    public static Arrow Parse(string str)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Value).Select(c => c.Pos)]);
    }

    public override bool IsSet => false;

    public override PosSet Cells { get; } = [.. cells];

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Circle(cells[0], cells[1..]),
        .. cells[1..].Select(c => new Shaft(cells[0], c, cells[1..].Remove(c))),
    ];

    public sealed class Circle(Pos circle, ImmutableArray<Pos> shaft) : Group(circle, shaft)
    {
        public override Candidates Restrict(Cells cells)
        {
            var min = 0;
            var max = 0;

            foreach (var val in Others.Select(o => cells[o]))
            {
                if (val is 0)
                {
                    min += 1;
                    max += _9;
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

    public sealed class Shaft(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public Pos Sum { get; } = sum;

        public int Size => Others.Length + 1;

        public override Candidates Restrict(Cells cells)
        {
            var s = cells[Sum];

            if (s is not 0 && s < Size)
            {
                return Candidates.None;
            }

            var min = 0;
            var max = 0;

            foreach (var val in Others.Select(o => cells[o]))
            {
                if (val is 0)
                {
                    min += 1;
                    max += _9;
                }
                else
                {
                    min += val;
                    max += val;
                }
            }

            var sums = s is 0 ? Candidates.AtLeast(Size) : [s];

            var allow = Candidates.None;

            foreach (var s_ in sums)
                allow |= Candidates.Between(s_ - max, s_ - min);

            return allow;
        }
    }
}
