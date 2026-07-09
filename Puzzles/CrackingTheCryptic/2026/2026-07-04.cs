namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_07_04 : CtcPuzzle
{
    public override string Title => "Green and Blue";

    public override string? Author => "Souradip Das";

    public override Uri? Url => new("https://youtu.be/CNGyml7iu_I");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        793│684│152
        546│172│938
        182│359│764
        ───┼───┼───
        618│523│479
        437│891│526
        925│467│381
        ───┼───┼───
        359│248│617
        864│715│293
        271│936│845
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        a..│...│...
        a..│X..│...
        ───┼───┼───
        b..│.Y.│...
        ...│..Y│...
        x..│.d.│Z..
        ───┼───┼───
        .y.│cc.│...
        ..y│...│...
        ...│z..│...
        a=b c=d x=y=z X=Y=Z
        """)
        + Lines.GermanWhisper("""
        ...│...│...
        ..a│bcd│efg
        ...│A.i│...
        ───┼───┼───
        ..B│...│ju.
        .C.│...│.v.
        D..│...│Mw.
        ───┼───┼───
        E..│..L│.x.
        F..│.K.│.y.
        GHI│J..│.z.
        """);
}
