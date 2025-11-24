using Sudoku.Restrictions;

namespace Sudoku.Common;

public static class DoubleArrow
{
    public static IEnumerable<Restriction> New(ImmutableArray<Pos> line)
    {
        var f = line[0];
        var s = line[^1];
        var shaft = line[1..^1];

        return
        [
            new End(f, s, shaft),
            new End(s, f, shaft),
            .. Group.Select(shaft, (a, o) => new Shaft(f, s, a, o))
        ];
    }

    private sealed class End(Pos appliesTo, Pos other, ImmutableArray<Pos> shaft) : Group(appliesTo, shaft)
    {
        public Pos Other { get; } = other;

        public override PosSet Links { get; } = [other, .. shaft];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = Ints.Zero;

            foreach (var cell in Others)
                total += cells[cell].Digits;

            total -= cells[Other].Digits;

            return total.Digits;
        }
    }

    private sealed class Shaft(Pos first, Pos second, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public Pos First { get; } = first;

        public Pos Second { get; } = second;

        public override PosSet Links { get; } = [first, second, .. others];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = cells[First].Digits;
            total += cells[Second].Digits;

            foreach (var cell in Others)
                total -= cells[cell].Digits;

            return total.Digits;
        }
    }
}
