namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_12 : CtcPuzzle
{
    public override string Title => "Crossroads On Another World";

    public override string? Author => "Jeff Wajes";

    public override Uri? Url => new("https://youtu.be/9p9EYYsbdp8");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        846│537│291
        715│492│683
        932│618│547
        ───┼───┼───
        628│941│375
        473│265│918
        591│873│462
        ───┼───┼───
        189│324│756
        254│786│139
        367│159│824
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        ...│ABC│...
        ...|ABC│...
        ───┼───┼───
        .LL│...│DD.
        .KK│...│EE.
        .JJ|...│FF.
        ───┼───┼───
        ...│IHG│...
        ...│IHG│...
        ...│...│...
        A = 10  B = 10  C = 10  D = 10  E = 10  F = 10  G = 10  H = 10  I = 10  J = 10  K = 10  L = 10
        """)
        + Lines.GermanWhisper("""
        ...│...│...
        ...│...│...
        ...│ABC│...
        ───┼───┼───
        ...│...│D..
        ...│...│E..
        ...│...│F..
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Lines.Renban("""
        ...│..C│...
        ...│...│CC.
        ...│...│.C.
        ───┼───┼───
        ...│...│..C
        A..│...│...
        A.B│...│...
        ───┼───┼───
        ...│BBB│...
        ...│...│...
        ...│...│...
        """)
       + Lines.SumsOfTen("""
        ...│A..│...
        .CB│...│...
        .D.│...│...
        ───┼───┼───
        E..│...│...
        ...│...│...
        ...│...│..j
        ───┼───┼───
        .b.│...│.i.
        a.c│...│gh.
        ...│def│...
        """)
       + KillerCages.Extend;
}
