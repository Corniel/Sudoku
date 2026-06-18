namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_01 : CtcPuzzle
{
    public override string Title => "African Daisies";

    public override string? Author => "Antiknight";

    public override Uri? Url => new("https://youtu.be/nDOET-_1e4c");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        273│946│185
        186│275│934
        495│138│267
        ───┼───┼───
        359│827│416
        862│314│579
        741│659│823
        ───┼───┼───
        524│761│398
        937│482│651
        618│593│742
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        // All ziplines have length 3, so they act as arrows
        + Lines.Arrow("""
        ..a│A.b│...
        ..a│...│Bb.
        ..c│.c.│d..
        ───┼───┼───
        ee.│C.D│..i
        E.g│...│d.I
        F..│G.H│..i
        ───┼───┼───
        ffg│.h.│h..
        Kk.│..l│L..
        k..│...│.l.
        """)
        + Lines.Nabmer("""
        ..A│A.B│...
        ..A│...│BB.
        ..C│.C.│D..
        ───┼───┼───
        EE.│C.D│..I
        E.G│...│D.I
        F..│G.H│..I
        ───┼───┼───
        FFG│.H.│H..
        KK.│..L│L..
        K..│...│.L.
        """);
}
