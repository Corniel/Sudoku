namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_08_10 : CtcPuzzle
{
    public override string Title => "Heartbeat";

    public override string? Author => "Stimim";

    public override Uri? Url => new("https://youtu.be/d2GE4rEe7uE");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        135│942│786
        978│615│342
        642│378│915
        ───┼───┼───
        463│589│271
        287│461│539
        519│237│468
        ───┼───┼───
        391│854│627
        724│196│853
        856│723│194
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        AB.│...│...
        AB.│...│...
        ...│...│...
        ───┼───┼───
        CC.│..E│.FG
        DD.│..E│.FG
        ...│.HH│...
        ───┼───┼───
        ...│...│..K
        ..I│JJ.│..K
        ..I│...│...
        A=10 B=10 C=10 D=10 E=10 F=10 G=10 H=10 I=10 J=10 K=10
        """)
        + Lines.Thermometer("""
        Aa.│..H│...
        Bb.│.G.│IJ.
        ...│...│...
        ───┼───┼───
        DE.│...│...
        de.│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Anti.Knight;
}
