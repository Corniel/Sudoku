namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_08 : CtcPuzzle
{
    public override string Title => "Starburst";

    public override string? Author => "Blobz";

    public override Uri? Url => new("https://youtu.be/12j4_BOirnY");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        962│473│518
        351│268│974
        784│951│623
        ───┼───┼───
        219│346│857
        876│592│341
        435│817│296
        ───┼───┼───
        648│129│735
        597│634│182
        123│785│469
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.SlowThermometer("""
        N..│.D.│..S
        .M.│.C.│.R.
        ..L│.B.│Q..
        ───┼───┼───
        ...│KAP│...
        IHG│F.f│gh.
        ...│kap│...
        ───┼───┼───
        ..l│.b.│q..
        .m.│.c.│.r.
        ...│.d.│..s
        """)
        + Lines.GermanWhisper("""
        .A.│..F│.J.
        L.B│D.G│I..
        OM.│C.H│.l.
        ───┼───┼───
        .P.│...│m..
        ..Q│...│...
        ...│...│op.
        ───┼───┼───
        ...│c.g│...
        ..b│d.f│h..
        .a.│...│.i.
        """);
}
