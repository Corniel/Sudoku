namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_07_02 : CtcPuzzle
{
    public override string Title => "Lost For Words";

    public override string? Author => "Br1312te";

    public override Uri? Url => new("https://youtu.be/Ks9cvaX91W4");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        964│837│251
        512│694│873
        783│215│964
        ───┼───┼───
        257│361│498
        839│452│716
        641│978│325
        ───┼───┼───
        398│126│547
        476│589│132
        125│743│689
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(3, 1).Clue(5)
        + pos(7, 7).Clue(3)
        + Lines.Renban("""
        AAA│AAB│BBB
        AKK│...│ccB
        A..│LLX│ccB
        ───┼───┼───
        A..│.Z.│X.B
        b..│.Z.│Y.B
        b..│.ZY│..a
        ───┼───┼───
        bCC│...│IIa
        bCC│...│..a
        bbb│baa│aaa
        """)
        + Lines.GermanWhisper("""
        ...│...│...
        ...│.K.│...
        .de│.L.│...
        ───┼───┼───
        ...│.M.│...
        .AB│C.c│ba.
        ...│.m.│...
        ───┼───┼───
        ...│.l.│...
        ...│.k.│...
        ...│...│...
        """)
        ;
}
