namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_02_19 : CtcPuzzle
{
    public override string Title => "Ubiquitous";

    public override string? Author => "Nicolas Duhail";

    public override Uri? Url => new("https://youtu.be/YypVfbIEfDE");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.New("""
        953│487│216
        186│235│479
        274│916│385
        ───┼───┼───
        731│524│968
        642│879│531
        598│163│724
        ───┼───┼───
        819│752│643
        467│398│152
        325│641│897
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        .AA│...│HHH
        A..│D.F│..H
        AEE│D.F│GGH
        ───┼───┼───
        BBB│CCC│III
        .aa│...│ii.
        a.a│fff│i.i
        ───┼───┼───
        bdd│e.g│hhj
        .b.│ecg│jjj
        ..b│cc.│j..
        A=B=C=D=E=F=G H=I
        a=b=c d=e=f=g=h i=j
        """);
}
