namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_10 : CtcPuzzle
{
    public override string Title => "Inbetween Taken";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/lzcnEnB5tvo");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        729│145│863
        185│326│974
        643│879│512
        ───┼───┼───
        938│652│147
        256│714│398
        471│983│256
        ───┼───┼───
        514│268│739
        362│597│481
        897│431│625
        """);

    public override Clues Clues => Clues.New("""
        ...│...│...
        ...│...│...
        ...│879│...
        ───┼───┼───
        ...│...│...
        ...│.1.│...
        ...│...│...
        ───┼───┼───
        5..│...│..9
        ...│...│...
        ...│...│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Between("""
        ..C│HN.│V..
        .BG│M.U│...
        AFL│.T.│..v
        ───┼───┼───
        EK.│S..│.u.
        J.R│...│t.n
        .Q.│..s│.mh
        ───┼───┼───
        P..│.r.│lgc
        ...│q.k│fb.
        ..p│.je│a..
        """)
        + Lines.Between("""
        ...│...│A..
        ...│...│.B.
        ...│...│..C
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        a..│...│...
        .b.│...│...
        ..c│...│...
       
        """)
        + Lines.Between("""
        ...│.AB│C..
        ...│...│...
        ...│...│..E
        ───┼───┼───
        ...│...│..F
        a..│...│..G
        b..│...│...
        ───┼───┼───
        c..│...│...
        ...│...│...
        ..e│fg.│...
        """);
}
