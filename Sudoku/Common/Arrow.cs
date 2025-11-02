using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Arrow(ImmutableArray<Pos> cells, bool isSet = false) : Rule(cells)
{
    public static Arrow ParseSet(string str) => Parse(str, true);

    public static Arrow Parse(string str, bool isSet = false)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Digit).Select(c => c.Pos)], isSet);
    }

    public override bool IsSet { get; } = isSet;

    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        ToCircle(cells[0], cells[1..], isSet),
        .. cells[1..].Select(c => ToShaft(c, cells, isSet)),
    ];

    private static Restriction ToCircle(Pos cell, ImmutableArray<Pos> cells, bool isSet)
        => isSet
        ? new CircleSet(cell, cells)
        : new Circle(cell, cells);

    private static Restriction ToShaft(Pos cell, ImmutableArray<Pos> cells, bool isSet)
        => isSet
        ? new ShaftSet(cells[0], cell, cells[1..].Remove(cell))
        : new Shaft(cells[0], cell, cells[1..].Remove(cell));

    private sealed class Circle(Pos circle, ImmutableArray<Pos> shaft) : Group(circle, shaft)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var min = 0;
            var max = 0;

            foreach (var digits in Others.Select(o => cells[o].Digits))
            {
                min += digits.First();
                max += digits.Last();
            }
            return Digits.Between(min, max);
        }
    }

    private sealed class CircleSet(Pos circle, ImmutableArray<Pos> shaft) : Group(circle, shaft)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var (known, unknw) = Sums(Others, cells);

            var min = known.Sum();
            var max = known.Sum();
            var missing = Others.Length - known.Count;
            min += unknw.Take(missing).Sum();
            max += unknw.Skip(unknw.Count - missing).Sum();

            return Digits.Between(min, max);
        }
    }

    private sealed class Shaft(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public Pos Sum { get; } = sum;

        public int Size => Others.Length + 1;

        public override Digits Restrict(SudokuCells cells)
        {
            var s = cells[Sum].Digit;

            if (s is not 0 && s < Size)
            {
                return Digits.None;
            }

            var min = 0;
            var max = 0;

            foreach (var digits in Others.Select(o => cells[o].Digits))
            {
                min += digits.First();
                max += digits.Last();
            }

            var sums = s is 0 ? Digits.AtLeast(Size) : [s];

            var allow = Digits.None;

            foreach (var s_ in sums)
                allow |= Digits.Between(s_ - max, s_ - min);

            return allow;
        }
    }

    private sealed class ShaftSet(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Cage(appliesTo, others)
    {
        public Pos Sum { get; } = sum;

        public int Minimum { get; } = triangle(others.Length + 1);

        public override Digits Restrict(SudokuCells cells)
        {
            var sum_ = cells[Sum].Digit;

            if (sum_ is 0)
            {
                var digits = Digits.None;

                for (var s_ = Minimum; s_ <= _9; s_++)
                    digits |= Restrict(cells, s_);

                return digits;
            }
            else if (sum_ < Minimum) return Digits.None;
            else return Restrict(cells, sum_);
        }
    }

    private static Summed Sums(IEnumerable<Pos> sum, SudokuCells cells)
    {
        Digits known = Digits.None;
        Digits unknw = Digits.None;

        foreach (var digits in sum.Select(c => cells[c].Digits))
        {
            if (digits.HasSingle)
                known |= digits;
            else
                unknw |= digits;
        }

        return new(known, unknw);
    }

    private readonly record struct Summed(Digits Known, Digits Unknown);
}
