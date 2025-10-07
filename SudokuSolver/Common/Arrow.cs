using SudokuSolver.Restrictions;

namespace SudokuSolver.Common;

public sealed class Arrow(ImmutableArray<Pos> cells, bool isSet = false) : Rule(cells)
{
    public static Arrow ParseSet(string str) => Parse(str, true);

    public static Arrow Parse(string str, bool isSet = false)
    {
        var path = Clues.Parse(str);
        return new([.. path.OrderBy(c => c.Value).Select(c => c.Pos)], isSet);
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
         public override Candidates Restrict(Graph graph)
        {
            var min = 0;
            var max = 0;

            foreach (var val in Others.Select(o => graph[o].Value))
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

    private sealed class CircleSet(Pos circle, ImmutableArray<Pos> shaft) : Group(circle, shaft)
    {
        public override Candidates Restrict(Graph graph)
        {
            var known = Candidates.None;
            var unknw = Candidates.None;

            foreach (var other in Others)
            {
                var val = graph[other].Value;
                if (val is 0)
                {
                    unknw |= graph[other].Candidates;
                }
                else
                {
                    known |= val;
                }
            }

            var min = known.Sum();
            var max = known.Sum();
            var missing = Others.Length - known.Count;
            min += unknw.Take(missing).Sum();
            max += unknw.Skip(unknw.Count - missing).Sum();

            return Candidates.Between(min, max);
        }
    }

    private sealed class Shaft(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public Pos Sum { get; } = sum;

        public int Size => Others.Length + 1;

        public override Candidates Restrict(Graph graph)
        {
            var s = graph[Sum].Value;

            if (s is not 0 && s < Size)
            {
                return Candidates.None;
            }

            var min = 0;
            var max = 0;

            foreach (var val in Others.Select(o => graph[o].Value))
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

    private sealed class ShaftSet(Pos sum, Pos appliesTo, ImmutableArray<Pos> others) : Cage(appliesTo, others)
    {
        public Pos Sum { get; } = sum;

        public int Minimum { get; } = triangle(others.Length + 1);

        public override Candidates Restrict(Graph graph)
        {
            var sum_ = graph[Sum].Value;

            if (sum_ is 0)
            {
                var candidates = Candidates.None;

                for (var s_ = Minimum; s_ <= _9; s_++)
                    candidates |= Restrict(graph, s_);

                return candidates;
            }
            else if (sum_ < Minimum) return Candidates.None;
            else return Restrict(graph, sum_);
        }
    }
}
