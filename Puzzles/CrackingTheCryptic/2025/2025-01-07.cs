namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_01_07 : CtcPuzzle
{
    public override string Title => "Sort of Miraculous";

    public override string? Author => "apetersen";

    public override Uri? Url => new("https://youtu.be/ztFZssfrEp4");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        246│579│813
        813│246│579
        579│813│246
        ───┼───┼───
        357│681│924
        924│357│681
        681│924│357
        ───┼───┼───
        468│792│135
        135│468│792
        792│135│468
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.AntiKnight
        + Couples.Ratio1_2((0, 2), (1, 2))
        + Houses.Boxes.SelectMany(EvenOdd);

    private static Rules EvenOdd(Box box)
        => (box.Index + 1).IsEven()
        ? box.Select((c, i) => new EvenCell(c, i, [.. box]))
        : box.Select((c, i) => new OddCell(c, i, [.. box]));

    private abstract class BoxCell(Pos appliesTo, int index, PosArray others, ImmutableArray<Digits> allowed) : Group(appliesTo, others)
    {
        public int Index { get; } = index;

        protected ImmutableArray<Digits> Allowed { get; } = allowed;

        protected abstract bool Restricted(int value);

        protected Digits Restrict(SudokuCells graph, int min, int max)
            => Allowed[Index] & Digits.Between(Min(graph, min), Max(graph, max));

        protected int Min(SudokuCells graph, int min)
        {
            for (var i = 0; i < Index; i++)
            {
                var val = graph[Others[i]].Digit;

                if (Restricted(val))
                    min = Math.Max(min, val);
            }
            return min + 1;
        }

        protected int Max(SudokuCells graph, int max)
        {
            for (var i = Index + 1; i < _9; i++)
            {
                var val = graph[Others[i]].Digit;

                if (Restricted(val))
                    max = Math.Min(max, val);
            }
            return max - 1;
        }
    }

    private sealed class EvenCell(Pos appliesTo, int index, PosArray cells)
            : BoxCell(appliesTo, index, cells, Evens)
    {
        public override Digits Restrict(SudokuCells cells)
            => Restrict(cells, -1, 11) | Digits.Odd;

        protected override bool Restricted(int value) => value is not 0 && value.IsEven();
    }

    private sealed class OddCell(Pos appliesTo, int index, PosArray cells)
            : BoxCell(appliesTo, index, cells, Odds)
    {
        public override Digits Restrict(SudokuCells cells)
            => Restrict(cells, 0, 10) | Digits.Even;

        protected override bool Restricted(int value) => value.IsOdd();
    }

    private static readonly ImmutableArray<Digits> Evens =
    [
        /* 0 */ [2],
        /* 1 */ [2, 4],
        /* 2 */ [2, 4, 6],
        /* 3 */ [2, 4, 6, 8],
        /* 4 */ [2, 4, 6, 8],
        /* 5 */ [2, 4, 6, 8],
        /* 6 */ [4, 6, 8],
        /* 7 */ [6, 8],
        /* 8 */ [8],
    ];

    private static readonly ImmutableArray<Digits> Odds =
    [
        /* 0 */ [1],
        /* 1 */ [1, 3],
        /* 2 */ [1, 3, 5],
        /* 3 */ [1, 3, 5, 7],
        /* 4 */ [1, 3, 5, 7, 9],
        /* 5 */ [3, 5, 7, 9],
        /* 6 */ [5, 7, 9],
        /* 7 */ [7, 9],
        /* 8 */ [9],
    ];
}
