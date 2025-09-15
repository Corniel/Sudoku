namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_11 : CtcPuzzle
{
    public override string Title => "Quadrants";
    public override string? Author => "Supware";
    public override Uri? Url => new("https://youtu.be/IEO4oA2-TTQ");

    public override Clues Clues { get; } = Clues.Parse("""
        .2.|...|...
        .13|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|7..
        ...|...|.89
        ...|...|...
        """);

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. Rules.Standard,
        .. Regions(),
    ];

    public override Cells Solution { get; } = Cells.Parse("""
        526|839|174
        813|724|695
        479|156|823
        ---+---+---
        635|918|247
        287|345|961
        194|267|358
        ---+---+---
        952|483|716
        361|572|489
        748|691|532
        """);

    private static IEnumerable<Region> Regions()
    {
        PosSet circles = [.. Clues.Parse("""
            ...|...|...
            ...|.1.|.1.
            ...|...|...
            ---+---+---
            ...|...|...
            .1.|.1.|.1.
            ...|...|...
            ---+---+---
            ...|...|...
            .1.|.1.|...
            ...|...|..1
            """).Select(c => c.Pos)];

        foreach(var p in Pos.All)
        {
            if (p.N() is { } n && p.W() is { } w)
            {
                PosSet cells = [p, n, w, p - 10];

                if (!circles.Any(cells.Contains))
                    yield return new Region([.. cells]);
            }
        }
    }

    public sealed class Region(ImmutableArray<Pos> cells) : Constraint
    {
        public override bool IsSet => false;

        public override PosSet Cells { get; } = [.. cells];

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            .. cells.Select(c => new Reduce(c, cells.Remove(c))),
        ];

        public sealed class Reduce(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;
            public ImmutableArray<Pos> Others { get; } = others;

            public Candidates Restrict(Cells cells)
            {
                var sum = 0;

                foreach (var val in Others.Select(o => cells[o]))
                {
                    if (val is 0) return Candidates._1_to_9;
                    sum += val;
                }
                return Allowed[sum % 4];
            }

            private static readonly ImmutableArray<Candidates> Allowed =
            [
                [4, 8],
                [3, 7],
                [2, 6],
                [1, 5, 9],
            ];
        }
    }
}
