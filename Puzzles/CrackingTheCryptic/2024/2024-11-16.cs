namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_11_16 : CtcPuzzle
{
    public override string Title => "80";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/2VFxX_4T4r0");

    public override O Duration => O.μs10;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│.2.│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        132│859│467
        965│174│328
        487│362│195
        ───┼───┼───
        298│531│746
        543│627│981
        716│498│532
        ───┼───┼───
        821│743│659
        359│216│874
        674│985│213
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Sums()
        + Products("""
        ..A│AA.│...
        ..B│...│.X.
        .B.│...│X..
        ───┼───┼───
        B..│..X│...
        ...│.X.│...
        ...│X..│..b
        ───┼───┼───
        ..X│...│.b.
        .X.│...│b..
        ...│.aa│a..
        """)
        + Products("""
        ...│.A.│...
        ...│..A│...
        ...│...│A..
        ───┼───┼───
        ...│X..│.A.
        a..│.X.│..A
        .a.│..X│...
        ───┼───┼───
        ..a│...│...
        ...│a..│...
        ...│.a.│...
        """);

    private static Rules Products(string grid)
        => Grid.NamedGroups(grid).SelectMany(Products);

    private static Rules Products(NamedGroup cells) =>
    [
        .. cells.Select(c => new Mask(c, [1, 2, 4, 5, 8])),
        .. cells.Select(c => new Product(c, cells.Cells.ToImmutableArray().Remove(c))),
    ];

    private sealed class Product(Pos appliesTo, PosArray others) : Group(appliesTo, others)
    {
        public override Digits Restrict(SudokuCells cells)
        {
            Ints product = [80];

            foreach (var digits in Others.Select(o => cells[o].Digits))
                product /= digits;

            return product.Digits;
        }
    }

    private static Rules Sums() =>
    [
        new LookupPair((0, 0), (0, 1), Prime1), new LookupPair((0, 1), (0, 0), Prime2),
        new LookupPair((0, 7), (0, 8), Prime1), new LookupPair((0, 8), (0, 7), Prime2),
        new LookupPair((8, 0), (8, 1), Prime1), new LookupPair((8, 1), (8, 0), Prime2),
        new LookupPair((8, 7), (8, 8), Prime1), new LookupPair((8, 8), (8, 7), Prime2),

        // A + B = 80
        .. Sum.New((0, 0), (0, 7), 07),
        .. Sum.New((0, 1), (0, 8), 10),

        // A + C = 80
        .. Sum.New((0, 0), (8, 0), 07),
        .. Sum.New((0, 1), (8, 1), 10),

        // B + D = 80
        .. Sum.New((0, 7), (8, 7), 07),
        .. Sum.New((0, 8), (8, 8), 10),
    ];

    private static readonly LookupDigits Prime1 = LookupPair.Init(d => d switch
    {
        1 => [1, 3, 4, 6, 7],    // 11,     31, 41,     61, 71
        3 => [1, 2, 4, 5, 7, 8], // 13, 23,     43, 53,     73, 83
        7 => [1, 3, 4, 6, 9],    // 17,     37, 47,     67,         97
        9 => [1, 2, 5, 7, 8],    // 19, 29,         59,     79, 89
        _ => Digits.None,
    });

    private static readonly LookupDigits Prime2 = LookupPair.Init(d => d switch
    {
        1 => [1, 3, 7, 9], // 11, 13, 17, 19
        2 => [3, 9], //           23,     29
        3 => [1, 7], //       31,     37
        4 => [1, 3, 7], //    41, 43, 47
        5 => [3, 9], //           53,     59
        6 => [1, 7], //       61,     67
        7 => [1, 3, 9], //    71, 73,     79
        8 => [3, 9], //           83,     89
        _ => [7], //                  97
    });
}
