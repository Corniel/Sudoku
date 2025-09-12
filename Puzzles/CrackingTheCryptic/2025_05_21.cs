namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_21 : CtcPuzzle
{
    public override string Title => "Stepped Themos";
    public override string? Author => "Aad van de Wetering";
    public override Uri? Url => new("https://youtu.be/AdSOJQ3huN0");

    public override Clues Clues { get; } = Clues.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        7..|...|...
        ..9|...|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        541|627|893
        982|531|674
        376|984|521
        ---+---+---
        625|493|718
        137|865|942
        498|172|356
        ---+---+---
        813|259|467
        754|316|289
        269|748|135
        """);

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        .. Rules.Standard,
        .. NonConsecutive.Create(),

        Thermometer.Parse("""
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|1..|...
            ---+---+---
            ..3|2..|...
            .54|...|...
            .6.|...|...
            """),
        Thermometer.Parse("""
            ...|...|...
            65.|...|...
            .43|...|...
            ---+---+---
            ..2|1..|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            """),
        Thermometer.Parse("""
            ...|...|.6.
            ...|...|45.
            ...|..2|3..
            ---+---+---
            ...|..1|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|...|...
            """),
        Thermometer.Parse("""
            ...|...|...
            ...|...|...
            ...|...|...
            ---+---+---
            ...|...|...
            ...|...|...
            ...|..1|2..
            ---+---+---
            ...|...|34.
            ...|...|.56
            ...|...|...
            """),
    ];

    public sealed class NonConsecutive(PosSet cells) : Constraint
    {
        public override bool IsSet => true;

        public override PosSet Cells { get; } = cells;

        public override ImmutableArray<Restriction> Restrictions { get; } = Reducer.Reducers([.. cells]);

        public sealed class Reducer(Pos appliesTo, ImmutableArray<Pos> others) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;
            
            public ImmutableArray<Pos> Others { get; } = others;

            public Candidates Restrict(Cells cells)
            {
                var index = Candidates.New(cells[Others[0]], cells[Others[1]]);
                return Loookup[index.GetHashCode()];
            }

            public static ImmutableArray<Restriction> Reducers(ImmutableArray<Pos> cells) =>
            [
                new Reducer(cells[0], cells.Remove(cells[0])),
                new Reducer(cells[1], cells.Remove(cells[1])),
                new Reducer(cells[2], cells.Remove(cells[2])),
            ];

            private static readonly ImmutableArray<Candidates> Loookup = Init();

            private static ImmutableArray<Candidates> Init()
            {
                var lookup = new Candidates[1 << 9 + 1];

                lookup[0] = Candidates._1_to_9;

                for (var i = 0; i < 9; i++)
                {
                    lookup[1 << i] = Candidates._1_to_9;
                }

                for (var i = 1; i <= 9; i++)
                {
                    for (var j = i; j <= 9; j++)
                    {
                        var index = Candidates.New(i, j).GetHashCode();

                        lookup[index] = (j - i) switch
                        {
                            0 => ~Candidates.New(i),
                            1 => ~Candidates.Between(i - 1, j + 1),
                            2 => ~Candidates.Between(i - 0, j + 0),
                            _ => Candidates._1_to_9,
                        };
                    }
                }
                return [.. lookup];
            }
        }

        public static IEnumerable<NonConsecutive> Create()
        {
            for (var f = 0; f < _9; f++)
            {
                for (var s = 0; s < 9; s += 3)
                {
                    yield return new NonConsecutive([(f, s), (f, s + 1), (f, s + 2)]);
                    yield return new NonConsecutive([(s, f), (s + 1, f), (s + 2, f)]);
                }
            }
        }
    }
}
