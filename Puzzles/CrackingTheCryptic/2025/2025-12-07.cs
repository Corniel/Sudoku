namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_07 : CtcPuzzle
{
    public override string Title => "Odd Way To Even Out";

    public override string? Author => "oskode";

    public override Uri? Url => new("https://youtu.be/r8N95dotL4M");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.Parse("""
        578│631│492
        634│892│175
        291│547│368
        ───┼───┼───
        825│916│743
        963│274│581
        417│358│926
        ───┼───┼───
        189│725│634
        356│489│217
        742│163│859
        """);

    public override Rules Constraints { get; }
        = Rules.Killer("""
        ...│..C│.DD
        ..A│A.C│...
        AAA│.CC│.BB
        ───┼───┼───
        ...│...│E.B
        b.e│e.E│E.B
        b.e│...│...
        ───┼───┼───
        bb.│cc.│aaa
        ...│c.a│a..
        dd.│c..│...

        A = 24  B = 18  C = 14  D = 11  E = 16
        a = 24  b = 22  c = 14  d = 11  e = 12
        """)

        + Quadruples.Parse("""
        AA.│...│.CC
        AA.│...│.CC
        ...│.BB│...
        ───┼───┼───
        ...│.BB│...
        ...│...│...
        ...│bb.│...
        ───┼───┼───
        ...│bb.│...
        aa.│...│.cc
        aa.│...│.cc

        A = 357  B = 14  C = 579
        a = 357  b = 23  c = 579
        """)
        + KillerCages.Extend;
}
