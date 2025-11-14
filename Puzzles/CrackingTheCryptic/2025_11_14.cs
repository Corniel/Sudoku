using Sudoku.Generics;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_14 : CtcPuzzle
{
    public override string Title => "Braiding Sweetgrass";

    public override string? Author => "Br1312te";

    public override Uri? Url => new("https://youtu.be/6o7caUPFY_s");

    public override O Duration => O.ms;

    public override Cells Solution => Cells.Parse("""
        3 8 4 6 2 7 9 5 1
        1 3 8 4 6 2 7 9 5
        5 1 3 8 4 6 2 7 9
        9 5 1 3 8 4 6 2 7
        7 9 5 1 3 8 4 6 2
        2 7 9 5 1 3 8 4 6
        6 2 7 9 5 1 3 8 4
        4 6 2 7 9 5 1 3 8
        8 4 6 2 7 9 5 1 3
        """);

    public override Rules Constraints { get; }
        = Rules.Jigsaw("""
            AAAAACEGG
            ABBBBCEGI
            ABCCCCEGI
            ABCDDDEGI
            ABCDEEEGI
            BBCDEFFGI
            DDDDEFGGI
            FFFFFFHHI
            HHHHHHHII
            """)
        + AntiKnight.All
        + NonConsecutives.Orthogonally()
        + Squares()
        + KillerCages.Parse(
            """
            .A.│...│...
            A..│...│...
            ...│...│...
            ───┼───┼───
            ...│...│...
            ...│...│..B
            ...│...│.B.
            ───┼───┼───
            ...│...│B..
            ...│..B│...
            ...│.B.│...
            A = 9  B = 21
            """,
            false)
        + Mask.Odd((8, 4));

    private static IEnumerable<Square> Squares()
    {
        foreach (var pos in Pos.All)
            if (pos.N() is { } n &&
                pos.W() is { } w &&
                n.W() is { } nw)
                yield return new Square([pos, n, w, nw]);
    }

    private sealed class Square(ImmutableArray<Pos> cells) : Rule(cells)
    {
        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. Group.Select(cells, (a, o) => new Reduction(a, o))
        ];

        private sealed class Reduction(Pos appliesTo, ImmutableArray<Pos> others) : Group(appliesTo, others)
        {
            public override Digits Restrict(SudokuCells cells)
            {
                var digits = Digits.None;
                foreach (var o in Others) digits |= cells[o].Digits;
                return Lookup[digits];
            }

            private static readonly DigitLookup<Digits> Lookup = Init();

            private static DigitLookup<Digits> Init()
            {
                var lookup = new DigitLookup<Digits>();
                Digits[] groups =
                [
                    [1, 4, 7],
                    [2, 5, 8],
                    [3, 6, 9],
                ];

                foreach (var digits in Digits.All)
                    foreach (var group in groups)
                        if ((digits & group).HasNone)
                            lookup[digits] |= group;

                foreach (var digits in Digits.All)
                    if (lookup[digits].HasNone)
                        lookup[digits] = Digits._1_to_9;

                return lookup;
            }
        }
    }
}
