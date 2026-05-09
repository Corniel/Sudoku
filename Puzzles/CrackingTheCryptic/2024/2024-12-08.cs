namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_12_08 : CtcPuzzle
{
    public override string Title => "Forune Cookie II";

    public override string? Author => "pieguy";

    public override Uri? Url => new("https://youtu.be/gD7gio1xuvU");

    public override O Duration => O.ms;

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Couples.Ratio1_2((4, 3), (5, 3))
        + Not7Nor13s()
        + Couples.WhiteDots("""
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
        """);

    public override Cells Solution { get; } = Cells.New("""
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

    public static Rules Not7Nor13s() => Dominos.Ort.SelectMany(Not7Nor13s);

    private static Rules Not7Nor13s(Domino domino) =>
    [
        new CellSet(domino, "!7 and !13"),
        .. new LookupPair(domino.A, domino.B, Lookup).Couple(),
    ];

    private static readonly LookupDigits Lookup = LookupPair.Init(
    [
        /* 0 */ Digits._1_to_9,
        /* 1 */ (Digits._1_to_9 ^ 1) ^ 6,
        /* 2 */ (Digits._1_to_9 ^ 2) ^ 5,
        /* 3 */ (Digits._1_to_9 ^ 3) ^ 4,
        /* 4 */ ((Digits._1_to_9 ^ 4) ^ 3) ^ 9,
        /* 5 */ ((Digits._1_to_9 ^ 5) ^ 2) ^ 8,
        /* 6 */ ((Digits._1_to_9 ^ 6) ^ 1) ^ 7,
        /* 7 */ (Digits._1_to_9 ^ 7) ^ 6,
        /* 8 */ (Digits._1_to_9 ^ 8) ^ 5,
        /* 9 */ (Digits._1_to_9 ^ 9) ^ 4,
    ]);
}
