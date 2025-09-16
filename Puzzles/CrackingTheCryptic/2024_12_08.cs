namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_12_08 : CtcPuzzle
{
    public override string Title => "Forune Cookie II";
    public override string? Author => "pieguy";
    public override Uri? Url => new("https://youtu.be/gD7gio1xuvU");

    public override Clues Clues { get; } = Clues.None;

    public override ImmutableArray<Constraint> Constraints { get; } =
    [
        new Ratio12((4, 3), (5, 3)),
        .. Rules.Standard,
        .. Not7Nor13s(),
        .. Consecutive.Parse("""
        .AA|BBE|FCC
        DD.|..E|F..
        .GG|HH.|II.
        ---+---+---
        J.K|.MN|PQ.
        J.K|.MN|PQ.
        LL.|.OO|RR.
        ---+---+---
        .SS|.WW|YZZ
        .TT|VVX|Y..
        .UU|..X|.aa
        """),
    ];

    public override Cells Solution { get; } = Cells.Parse("""
        421|873|965
        563|192|847
        789|564|123
        ---+---+---
        246|957|381
        315|648|279
        897|321|456
        ---+---+---
        154|789|632
        978|236|514
        632|415|798
        """);

    public static IEnumerable<Not7Nor13> Not7Nor13s()
    {
        foreach (var p in Pos.All)
        {
            if (p.N() is { } n) yield return new Not7Nor13(p, n);
            if (p.W() is { } w) yield return new Not7Nor13(p, w);
        }
    }

    public sealed class Ratio12(Pos a, Pos b) : Constraint
    {
        public override bool IsSet => true;

        public override PosSet Cells { get; } = [a, b];

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            new Reduce(a, b),
            new Reduce(b, a),
        ];

        public sealed class Reduce(Pos appliesTo, Pos other) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;
            public Pos Other { get; } = other;

            public Candidates Restrict(Cells cells) => Lookup[cells[Other]];

            private static readonly ImmutableArray<Candidates> Lookup =
            [
                /* 0 */ Candidates._1_to_9,
                /* 1 */ [2],
                /* 2 */ [1, 4],
                /* 3 */ [6],
                /* 4 */ [2, 8],
                /* 5 */ default,
                /* 6 */ [3],
                /* 7 */ default,
                /* 8 */ [4],
                /* 9 */ default,
            ];
        }
    }

    public sealed class Not7Nor13(Pos a, Pos b) : Constraint
    {
        public override bool IsSet => true;

        public override PosSet Cells { get; } = [a, b];

        public override ImmutableArray<Restriction> Restrictions { get; } =
        [
            new Reduce(a, b),
            new Reduce(b, a),
        ];

        public sealed class Reduce(Pos appliesTo, Pos other) : Restriction
        {
            public Pos AppliesTo { get; } = appliesTo;
            public Pos Other { get; } = other;

            public Candidates Restrict(Cells cells) => Lookup[cells[Other]];

            private static readonly ImmutableArray<Candidates> Lookup =
            [
                /* 0 */ Candidates._1_to_9,
                /* 1 */ (Candidates._1_to_9 ^ 1) ^ 6,
                /* 2 */ (Candidates._1_to_9 ^ 2) ^ 5,
                /* 3 */ (Candidates._1_to_9 ^ 3) ^ 4,
                /* 4 */ ((Candidates._1_to_9 ^ 4) ^ 3) ^ 9,
                /* 5 */ ((Candidates._1_to_9 ^ 5) ^ 2) ^ 8,
                /* 6 */ ((Candidates._1_to_9 ^ 6) ^ 1) ^ 7,
                /* 7 */ (Candidates._1_to_9 ^ 7) ^ 6,
                /* 8 */ (Candidates._1_to_9 ^ 8) ^ 5,
                /* 9 */ (Candidates._1_to_9 ^ 9) ^ 4,
            ];
        }
    }
}
