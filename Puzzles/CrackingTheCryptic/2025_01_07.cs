using SudokuSolver.Houses;
using SudokuSolver.Restrictions;
using System.Numerics;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_01_07 : CtcPuzzle
{
    public override string Title => "Sort of Miraculous";
    public override string? Author => "apetersen";
    public override Uri? Url => new("https://youtu.be/ztFZssfrEp4");
    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.Parse("""
        246|579|813
        813|246|579
        579|813|246
        ---+---+---
        357|681|924
        924|357|681
        681|924|357
        ---+---+---
        468|792|135
        135|468|792
        792|135|468
        """);

    public override Rules Constraints { get; } =
        Rules.AntiKnight
        + new Ratio1_2((0, 2), (1, 2))
        + Boxes();
    

    private static IEnumerable<Rule> Boxes()
    {
        foreach (var b in Box.All)
            yield return (b.Index & 1) == 1
                ? new EvenBox([.. b.Cells])
                : new OddBox([.. b.Cells]);
    }

    public sealed class EvenBox(ImmutableArray<Pos> cells) : Set(cells)
    {
        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. cells.Select((c, i) => new EvenCell(c, i, cells)),
        ];
    }

    public sealed class OddBox(ImmutableArray<Pos> cells) : Set(cells)
    {
        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. cells.Select((c, i) => new OddCell(c, i, cells)),
        ];
    }

    private abstract class BoxCell(Pos appliesTo, int index, ImmutableArray<Pos> others, ImmutableArray<Candidates> allowed) : Group(appliesTo, others)
    {
        public int Index { get; } = index;

        protected ImmutableArray<Candidates> Allowed { get; } = allowed;

        protected abstract bool Restricted(int value);

        protected Candidates Restrict(Cells cells, int min, int max)
            => Allowed[Index] & Candidates.Between(Min(cells, min), Max(cells, max));

        protected int Min(Cells cells, int min)
        {
            for (var i = 0; i < Index; i++)
            {
                var val = cells[Others[i]];

                if (Restricted(val))
                    min = Math.Max(min, val);
            }
            return min + 1;
        }

        protected int Max(Cells cells, int max)
        {
            for (var i = Index + 1; i < _9; i++)
            {
                var val = cells[Others[i]];

                if (Restricted(val))
                    max = Math.Min(max, val);
            }
            return max - 1;
        }
    }

    private sealed class EvenCell(Pos appliesTo, int index, ImmutableArray<Pos> cells)
            : BoxCell(appliesTo, index, cells, Evens)
    {
        public override double Bits => Info.Avg(Allowed[Index].Count / 20d);

        public override Candidates Restrict(Cells cells)
            => Restrict(cells, -1, 11) | Candidates.Odd;

        protected override bool Restricted(int value) => value is not 0 && value.IsEven();
    }

    private sealed class OddCell(Pos appliesTo, int index, ImmutableArray<Pos> cells)
            : BoxCell(appliesTo, index, cells, Odds)
    {
        public override double Bits => Info.Avg(Allowed[Index].Count / 20d);

        public override Candidates Restrict(Cells cells)
            => Restrict(cells, 0, 10) | Candidates.Even;

        protected override bool Restricted(int value) => value.IsOdd();
    }

    private static readonly ImmutableArray<Candidates> Evens =
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

    private static readonly ImmutableArray<Candidates> Odds =
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
