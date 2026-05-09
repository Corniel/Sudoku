namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_04 : CtcPuzzle
{
    public override string Title => "Farrago";

    public override string? Author => "Nicolas Dubail";

    public override Uri? Url => new("https://youtu.be/PcJNQCckiKs");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        346│951│287
        528│674│931
        791│328│645
        ───┼───┼───
        852│437│196
        637│519│428
        914│862│753
        ───┼───┼───
        263│745│819
        185│296│374
        479│183│562
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│.1.│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.Cages("""
        ..B│CCC│E..
        .BF│GDD│.EE
        B.F│Gff│ggE
        ───┼───┼───
        A.I│Hiihh.
        .AI│H..│...
        ..A│aa.│...
        ───┼───┼───
        ...│bb.│...
        ..c│bx.│.y.
        ..c│bxx│yyy
        A=B=C a=b=c D=E F=G=H=I f=g=h=i x=y
        """)
        + Jigsaw.New("""
        ..a│aaa│b..
        .ac│cbb│.bb
        a.C│cdd│ddb
        ───┼───┼───
        a.c│cdd│dd.
        .ac│c.e│e..
        ..a│ffe│e..
        ───┼───┼───
        ...│ffe│e..
        ..f│fge│eg.
        ..f│fgg│ggg
        """);
}
