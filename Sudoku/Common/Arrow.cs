using Sudoku.Restrictions;

namespace Sudoku.Common;

public sealed class Arrow(ImmutableArray<Pos> cells) : Rule(cells)
{
    public override ImmutableArray<Restriction> Restrictions { get; } =
    [
        new Circle(cells[0], cells[1..]),
        .. Group.Select(cells[1..], (appliesTo, others) => new Shaft(cells[0], appliesTo, others))
    ];

    private sealed class Circle(Pos circle, ImmutableArray<Pos> shaft) : Group(circle, shaft)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            var total = Ints.Zero;

            foreach (var cell in Others)
                total += cells[cell].Digits;

            return total.Digits;
        }
    }

    private sealed class Shaft(Pos circle, Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
    {
        public Pos Circle { get; } = circle;

        public override PosSet Links { get; } = [circle, .. others];

        public override Digits Restrict(SudokuCells cells)
        {
            Ints total = cells[Circle].Digits;

            foreach (var cell in Others)
                total -= cells[cell].Digits;

            return total.Digits;
        }
    }
}
