namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_16 : CtcPuzzle
{
    public override string Title => "Four Octagons";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/vNfwa4wPliY");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        693│714│285
        275│968│341
        841│352│769
        ───┼───┼───
        529│637│814
        468│291│537
        317│485│926
        ───┼───┼───
        956│123│478
        134│879│652
        782│546│193
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│.1.│...
        .AA│...│B4.
        .AA│...│BB.
        ───┼───┼───
        ...│.X.│...
        ...│XXX│...
        ...│4X.│...
        ───┼───┼───
        .CC│...│DD.
        .C4│...│DD.
        ...│...│...
        A=17 B=16 C=14 D=22 X=23
        """)
        + Lines.Delta6("""
        .AB│...│ab.
        H..│C.h│..c
        G..│D.g│..d
        ───┼───┼───
        .FE│...│fe.
        ...│...│...
        .KL│...│kl.
        ───┼───┼───
        R..│M.r│..m
        Q..│N.q│..n
        .PO│...│po.
        """)
        + Lines.Delta6("""
        .B.│...│b..
        A..│..a│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        .L.│...│l..
        ───┼───┼───
        K..│..k│...
        ...│...│...
        ...│...│...
        """);
}
